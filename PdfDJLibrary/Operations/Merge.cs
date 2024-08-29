﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace PdfDJ.Library.Operations
{
    /// <summary>
    /// 
    /// </summary>
    public class Merge
    {
        private static readonly NLog.Logger l = NLog.LogManager.GetCurrentClassLogger();
        private Merge() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <param name="outputFolder"></param>
        /// <returns></returns>
        public static FileInfo Merge2Files(string file1, string file2, string outputFolder)
        {
            FileInfo fileMerged = null;

            try
            {

                l.Info("start merging.. {0} {1} {2}", file1, file2, outputFolder);

                if (string.IsNullOrEmpty(outputFolder)) throw new Exception("folder outpu vuoto");
                DirectoryInfo od = new DirectoryInfo(outputFolder);
                if (!od.Exists) throw new Exception(string.Format("folder {0} inesistente", od.FullName));

                string of = Path.Combine(od.FullName, string.Format("{0}.pdf", Guid.NewGuid()));
                fileMerged = new FileInfo(of);
                if (fileMerged.Exists) throw new Exception(string.Format("file {0} esistente", fileMerged.FullName));

                //PdfDocument pdfDocument = new PdfDocument(new PdfReader(file1), new PdfWriter(fileMerged.FullName));
                //PdfDocument pdfDocument2 = new PdfDocument(new PdfReader(file2));

                //PdfMerger merger = new PdfMerger(pdfDocument);
                //merger.Merge(pdfDocument2, 1, pdfDocument2.GetNumberOfPages());

                //pdfDocument2.Close();
                //pdfDocument.Close();


                //////////////////////////////////////////
                string[] fileNames = { file1, file2 }; 
                using (var mergedDocument = new PdfDocument(new PdfWriter(fileMerged.FullName)))
                {
                    foreach (var fileName in fileNames)
                    {
                        using (var pdfDocument = new PdfDocument(new PdfReader(fileName)))
                        {
                            pdfDocument.CopyPagesTo(1, pdfDocument.GetNumberOfPages(), mergedDocument);
                        }
                    }
                }
                //////////////////////////////////////////

            }
            catch (Exception ex)
            {
                l.Error("Error {0}", ex.Message);
                throw;
            }
            return fileMerged;
        }
    }
}
