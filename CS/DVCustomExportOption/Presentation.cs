﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.PowerPoint;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;

namespace DVCustomExportOption {
    // https://community.devexpress.com/blogs/seth/archive/2011/02/14/exporting-reports-to-powerpoint.aspx
    public class Presentation {
        Microsoft.Office.Interop.PowerPoint.Application _powerPoint;
        Microsoft.Office.Interop.PowerPoint.Presentation _presentation;
        
        ~Presentation() {
            _powerPoint = null;
            _presentation = null;
             GC.Collect();
             GC.WaitForPendingFinalizers();
        }

        public Presentation(Size paperSize) {
            _powerPoint = new Microsoft.Office.Interop.PowerPoint.Application();
            _presentation = _powerPoint.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoFalse);
            _presentation.PageSetup. SlideHeight = paperSize.Height;
            _presentation.PageSetup.SlideWidth = paperSize.Width;
        }
        
        private int _slideNo = 0;
        public void AddPage(string fileName) {
            _slideNo++;
            var slide = _presentation.Slides.Add(_slideNo, Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutBlank);
            slide.Shapes.AddPicture(fileName,
            Microsoft.Office.Core.MsoTriState.msoFalse,
            Microsoft.Office.Core.MsoTriState.msoTrue, 0, 0);
        }

        public void SaveAs(string filename) {
            if (File.Exists(filename))
            File.Delete(filename);
            _presentation.SaveAs(filename);
            _presentation.Close();
            _powerPoint.Quit();
        }
    }
}