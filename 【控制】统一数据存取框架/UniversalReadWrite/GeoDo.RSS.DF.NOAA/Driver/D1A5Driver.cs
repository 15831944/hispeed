﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OSGeo.GDAL;
using GeoDo.RSS.DF.GDAL;
using GeoDo.RSS.Core.DF;
using System.ComponentModel.Composition;

namespace GeoDo.RSS.DF.NOAA
{
    //[Export(typeof(IGeoDataDriver)), ExportMetadata("VERSION", "1")]
    public class D1A5Driver:GDALRasterDriver,I1A5Driver
    {
        public D1A5Driver()
            : base()
        {
            _name = "NOAA_1A5";
            _fullName = "NOAA 1A5 Data Driver";
        }

        public D1A5Driver(string name, string fullName)
            :base(name,fullName)
        { 
        }

        protected override IGeoDataProvider BuildDataProvider(string fileName, enumDataProviderAccess access, params object[] args)
        {
            return new D1A5DataProvider(fileName, this, access);
        }

        protected override IGeoDataProvider BuildDataProvider(string fileName, byte[] header1024, enumDataProviderAccess access, params object[] args)
        {
            return new D1A5DataProvider(fileName,null,this,access);
        }

        public override IRasterDataProvider Create(string fileName, int xSize, int ySize, int bandCount, enumDataType dataType, params object[] options)
        {
            throw new NotImplementedException();
        }

        protected override bool IsCompatible(string fileName, byte[] header1024, params object[] args)
        {
            string fileExtension = Path.GetExtension(fileName).ToUpper();
            return D1A5Header.Is1A5(header1024, fileExtension);
        }
    }
}
