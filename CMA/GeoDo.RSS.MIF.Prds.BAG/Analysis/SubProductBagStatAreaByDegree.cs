﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoDo.RSS.Core.DF;
using GeoDo.RSS.MIF.Core;
using System.Drawing;
using GeoDo.RSS.MIF.Prds.Comm;
using System.IO;

namespace GeoDo.RSS.MIF.Prds.BAG
{
    public class SubProductBagStatAreaByDegree : CmaMonitoringSubProduct
    {
        private bool _isComputCloudArea = false;

        public SubProductBagStatAreaByDegree(SubProductDef subProductDef)
            : base(subProductDef)
        {
            _identify = subProductDef.Identify;
            _isBinary = true;
            _algorithmDefs = new List<AlgorithmDef>(subProductDef.Algorithms);
        }

        public override IExtractResult Make(Action<int, string> progressTracker)
        {
            if (_argumentProvider == null || _argumentProvider.GetArg("SelectedPrimaryFiles") == null)
                return null;
            string[] bpcdFile = GetStringArray("SelectedPrimaryFiles");
            if (bpcdFile == null || bpcdFile.Length < 1)
                return null;
            if (_argumentProvider.GetArg("AlgorithmName").ToString() == "BCDA")
            {   
                List<string> regions = new List<string>();
                string outputIdentify = _argumentProvider.GetArg("OutFileIdentify").ToString();
                if (string.IsNullOrEmpty(outputIdentify))
                {
                    return null;  
                }
                else
                {
                    if (outputIdentify == "CDAH")
                        regions.AddRange(new string[] { "0%~30%", "30%~60%", "60%~100%" });
                    else if (outputIdentify == "CDAE")
                        regions.AddRange(new string[] { "0%~10%", "10%~20%", "20%~30%", "30%~40%", "40%~50%", "50%~60%", "60%~70%", "70%~80%", "80%~90%", "90%~100%" });
                    else
                        regions = _argumentProvider.GetArg("CovertDegreeRegion") as List<string>;
                }
                return StaticAreaByDegree(regions, bpcdFile[0],outputIdentify);
            }
            return null;
        }

        private IExtractResult StaticAreaByDegree(List<string> rangeList,string fileName,string outputIdentify)
        {
            IRasterDataProvider dataProvider = null;
            IRasterDataProvider cloudProvider = null;
            List<string[]> resultList = new List<string[]>();
            try
            {
                dataProvider = GeoDataDriver.Open(fileName) as IRasterDataProvider;
                //获取分区AOI
                Dictionary<string, int[]> aois = GetAOIForRaster(dataProvider);
                if (aois != null && aois.Count > 0)
                {
                    foreach (string region in aois.Keys)
                    {
                        List<string[]> result = StaticAreaByRegions(rangeList, cloudProvider, dataProvider, aois[region], region);
                        if (result != null)
                            resultList.AddRange(result);
                    }
                }
                //整个区域
                List<string[]> statresult = StaticAreaByRegions(rangeList,resultList,aois.Count, "整个湖区");
                if (statresult != null)
                    resultList.AddRange(statresult);

                if (resultList != null && resultList.Count != 0)
                {
                    float resolution = dataProvider.ResolutionX;
                    string title = "统计日期：" + DateTime.Now.ToShortDateString();
                    RasterIdentify id = new RasterIdentify(fileName);
                    if(id.OrbitDateTime!=null)
                        title += "    轨道日期：" + id.OrbitDateTime.ToShortDateString();
                    string[] columns = new string[] { "区域名称", "覆盖度范围", "总覆盖面积（平方公里）", "实际覆盖面积（平方公里）"};
                    IStatResult result = new StatResult(title, columns, resultList.ToArray());
                    string filename = StatResultToFile(new string[]{fileName}, result, "BAG", outputIdentify, "蓝藻按强度统计面积",null, 1,false,0);
                    return new FileExtractResult("BCDA", filename);
                }
            }
            finally
            {
                if (cloudProvider != null)
                    cloudProvider.Dispose();
                if (dataProvider != null)
                    dataProvider.Dispose();
            }
            return null;
        }

        private List<string[]> StaticAreaByRegions(List<string> covertDegreeRegions, List<string[]> resultList,int regionCount, string regionName)
        {
            float area, totalAreas=0, actualAreas=0;
            List<string> statresult = new List<string>();
            List<string[]> result = new List<string[]>();
            for (int i = 0; i < covertDegreeRegions.Count; i++)
            {
                statresult.Clear();
                if (i == 0)
                {
                    statresult.Add(regionName);
                }
                else
                    statresult.Add("");
                statresult.Add(covertDegreeRegions[i]);
                area = 0;
                for (int j = 1; j <=regionCount; j++)
                {
                    area += float.Parse(resultList[(covertDegreeRegions.Count + 1) * (j - 1) + i][2]);
                    
                }
                totalAreas += area;
                statresult.Add(area.ToString());
                area = 0;
                for (int j = 1; j <= regionCount; j++)
                {
                    area += float.Parse(resultList[(covertDegreeRegions.Count + 1) * (j - 1) + i][3]);

                }
                actualAreas += area;
                statresult.Add(area.ToString());
                result.Add(statresult.ToArray());
            }
            result.Add(new string[] { "", "合计", totalAreas.ToString(), actualAreas.ToString() });
            return result;
        }

        private List<string[]> StaticAreaByRegions(List<string> covertDegreeRegions,IRasterDataProvider cloudProvider,IRasterDataProvider coverDegreeProvider, int[] aoi,string regionName)
        {
            if (covertDegreeRegions == null || covertDegreeRegions.Count == 0)
                return null;
            List<string[]> resultList = new List<string[]>();
            double totalAreas = 0;
            double actualAreas = 0;
            for (int i = 0; i < covertDegreeRegions.Count; i++)
            {
                List<string> statresult = new List<string>();
                float[] minmax = BAGStatisticHelper.GetCovertDegreeValue(covertDegreeRegions[i]);
                //累计指定等级的总覆盖面积
                double totalArea = StatAccumulativeTotalArea(coverDegreeProvider, aoi, minmax[0], minmax[1]);
                //累计计算指定等级的实际覆盖面积
                double actualArea = StatAccumulativeActualArea(coverDegreeProvider,aoi, minmax[0], minmax[1]);
                totalAreas += totalArea;
                actualAreas += actualArea;
                if (i == 0)
                {
                    statresult.Add(regionName);
                }
                else
                    statresult.Add("");
                statresult.Add(covertDegreeRegions[i]);
                statresult.Add(Math.Round(totalArea, 2).ToString());
                statresult.Add(Math.Round(actualArea, 2).ToString());
                resultList.Add(statresult.ToArray());
            }
            resultList.Add(new string[] {"","合计",totalAreas.ToString(),actualAreas.ToString()});
            return resultList;
        }

        /// <summary>
        /// 统计累计总覆盖面积
        /// </summary>
        /// <returns></returns>
        public double StatAccumulativeTotalArea(IRasterDataProvider[] dataProvider,int[] aoi, float min, float max)
        {
            if (dataProvider == null || dataProvider.Count() < 1)
                throw new Exception("数据集集合为空，不能计算累计覆盖面积！");
            double retArea = 0;
            foreach (IRasterDataProvider ds in dataProvider)
                retArea += StatAccumulativeTotalArea(ds,aoi,min, max);
            return retArea;
        }

        public double StatAccumulativeTotalArea(IRasterDataProvider dataProvider,int[] aoi,float min, float max)
        {
            if (dataProvider == null)
                throw new Exception("数据集为空，不能计算累计覆盖面积！");
            return CalcTotalConvertArea(dataProvider,aoi, min, max);
        }

        /// <summary>
        /// 计算指定等级的总覆盖面积
        /// 例如:重度(60%-100%)
        /// 比较条件为：minConvertDegree< x && x<=  maxConvertDegree
        /// </summary>
        /// <param name="minConvertDegree"></param>
        /// <param name="maxConvertDegree"></param>
        /// <returns></returns>
        public double CalcTotalConvertArea(IRasterDataProvider dataProvider,int[] aoi, float minCovertDegree, float maxCovertDegree)
        {
            double convertArea = 0;
            IRasterOperator<float> rasterOper = new RasterOperator<float>();
            int count = rasterOper.Count(dataProvider, aoi, (value) =>
                {
                    if (minCovertDegree < value && value <= maxCovertDegree)
                        return true;
                    else return false;
                });
            double pixelArea = BAGStatisticHelper.CalPixelArea(dataProvider.ResolutionX);
            convertArea = count * pixelArea;
            return convertArea;
        }


        /// <summary>
        /// 计算指定等级的总实际覆盖面积
        /// 比较条件为：minConvertDegree< x && x<=  maxConvertDegree
        /// </summary>
        /// <param name="dataProvider">像元覆盖度文件DataProvider</param>
        /// <param name="minConvertDegree"></param>
        /// <param name="maxConvertDegree"></param>
        /// <returns></returns>
        public double CalcActualConvertArea(IRasterDataProvider dataProvider,int[] aoi, float minConvertDegree, float maxConvertDegree)
        {
            if (dataProvider == null || minConvertDegree > maxConvertDegree)
                return 0;
            double convertArea = 0;
            double pixelArea = BAGStatisticHelper.CalPixelArea(dataProvider.ResolutionX);
            ArgumentProvider ap = new ArgumentProvider(dataProvider,null);
            Size size = new Size(dataProvider.Width, dataProvider.Height);
            Rectangle rect = AOIHelper.ComputeAOIRect(aoi, size);
            RasterPixelsVisitor<float> visitor = new RasterPixelsVisitor<float>(ap);
            visitor.VisitPixel(rect,aoi,new int[] { 1 },
                (index, values) =>
                {
                    if (values[0] <= maxConvertDegree && values[0] > minConvertDegree)
                    {
                        convertArea += (pixelArea * values[0]);
                    }
                });
            return convertArea;
        }

        /// <summary>
        /// 统计累计实际覆盖面积
        /// </summary>
        /// <returns></returns>
        public double StatAccumulativeActualArea(IRasterDataProvider[] dataProvider,int[] aoi, float min, float max)
        {
            if (dataProvider == null || dataProvider.Count() < 1)
                throw new Exception("数据集集合为空，不能计算累计实际覆盖面积！");
            double retArea = 0;
            foreach (IRasterDataProvider ds in dataProvider)
                retArea += StatAccumulativeActualArea(ds,aoi, min, max);
            return retArea;
        }

        public double StatAccumulativeActualArea(IRasterDataProvider ds,int[] aoi, float min, float max)
        {
            if (ds == null)
                throw new Exception("数据集为空，不能计算累计覆盖面积！");
            return CalcActualConvertArea(ds,aoi, min, max);
        }

        private Dictionary<string, int[]> GetAOIForRaster(IRasterDataProvider dataProvider)
        {
            Dictionary<string, string> templates = BAGStatisticHelper.GetAOITemplateList();
            if (templates == null || templates.Count == 0)
                return null;
            Dictionary<string, int[]> aoiValues = new Dictionary<string, int[]>();
            foreach (string templateName in templates.Keys)
            {
                //分湖区
                Dictionary<string, int[]> aois = VectorTemplateToAOIConvertor.GetFeatureAOIIndex(templates[templateName],
                    "NAME", dataProvider.CoordEnvelope, new Size(dataProvider.Width, dataProvider.Height));
                if (aois != null && aois.Count != 0)
                {
                    //List<int> indexForOneLake=new List<int>();
                    foreach (string key in aois.Keys)
                    {
                        aoiValues.Add(key, aois[key]);
                        //if (indexForOneLake.Count == 0)
                        //{
                        //    indexForOneLake.AddRange(aois[key]);
                        //}
                        //else
                        //{
                        //    for (int i = 0; i < aois[key].Length; i++)
                        //    {
                        //        if (indexForOneLake.Contains(aois[key][i]))
                        //            continue;
                        //        else
                        //            indexForOneLake.Add(aois[key][i]);
                        //    }
                        //}
                    }
                    //aoiValues.Add(templateName, indexForOneLake.ToArray());
                } 
            }
            return aoiValues;
        }
    }
}
