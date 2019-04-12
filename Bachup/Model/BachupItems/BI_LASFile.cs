﻿using Ionic.Zip;
using System;
using System.IO;
using System.Windows.Forms;

namespace Bachup.Model.BachupItems
{
    class BI_LASFile : BachupItem
    {

        public BI_LASFile(string name, string source, Guid bachupGroupID) : base(name, source, bachupGroupID)
        {
            _bachupType = BachupType.TXT;
            _sourceType = BachupItemSourceTypes.File;
        }

        #region Methods

        public override void CopyData()
        {
            foreach (string destination in Destinations)
            {
                if (Directory.Exists(destination))
                {
                    string bachupLocation = GenerateBachupLocation(destination);

                    if (bachupLocation == "")
                        continue;

                    string fileName = Path.GetFileName(Source);
                    string destFile = Path.Combine(bachupLocation, fileName);

                    File.Copy(Source, destFile);
                }
            }
        }

        public override void CopyDataWithZip()
        {
            foreach (string destination in Destinations)
            {
                if (Directory.Exists(destination))
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        string bachupLocation = GenerateBachupLocation(destination);

                        if (bachupLocation == "")
                            continue;

                        string zippedBachupLocation = Path.Combine(bachupLocation, Path.GetFileNameWithoutExtension(Source) + ".zip");

                        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                        zip.AddItem(Source, "");
                        zip.Save(zippedBachupLocation);
                    }
                }
            }
        }

        public override bool IsFileLocked()
        {
            return false;
        }

        public override void RepairSource()
        {
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "las file (*.las)|*.las";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    Source = openFileDialog.FileName;
                }
            }
        }

        #endregion
    }
}