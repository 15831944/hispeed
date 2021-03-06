﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoDo.RSS.MIF.Core;
using System.IO;
using GeoDo.RSS.MIF.Prds.Comm;

namespace GeoDo.RSS.MIF.Prds.DST
{
    public class SubProductSTATArea : CmaMonitoringSubProduct
    {

        public SubProductSTATArea(SubProductDef subDef)
            : base(subDef)
        {
        }

        public override IExtractResult Make(Action<int, string> progressTracker)
        {
            if (_argumentProvider == null)
                return null;
            if (_argumentProvider.GetArg("AlgorithmName") == null)
                return null;
            if (_argumentProvider.GetArg("AlgorithmName").ToString() != "STATAlgorithm")
                return null;
            //按照Instance执行统计操作
            string instanceIdentify = _argumentProvider.GetArg("OutFileIdentify") as string;
            if (instanceIdentify != null)
            {
                SubProductInstanceDef instance = FindSubProductInstanceDefs(instanceIdentify);
                if (instance == null || instance.OutFileIdentify == "CCCA" || instance.OutFileIdentify == "CCAR")
                {
                    return STATAlgorithm();
                }
                else
                    return StatRaster<short>(instance, (v) => { return v == 1; }, true, progressTracker);
            }
            return null;
        }

        private IExtractResult STATAlgorithm()
        {
            return AreaStatResult<Int16>("沙尘", "DST", (v) => { return v == 1; });
        }
    }
}
