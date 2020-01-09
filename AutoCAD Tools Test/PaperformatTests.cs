using System;
using AutoCADTools.PrintLayout;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoCADToolsTest
{
    [TestClass]
    public class PaperformatTest
    {
        [TestMethod]
        public void CreateFormats()
        {
            var paper = PaperformatFactory.GetPaperformatTextfield(new Size(250, 160));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldA4Horizontal));
            Assert.AreEqual(paper.ViewportSizeModel, PaperformatTextfieldA4Horizontal.MaximumViewportSize);
            paper = PaperformatFactory.GetPaperformatTextfield(new Size(160, 250));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldA4Vertical));
            Assert.AreEqual(paper.ViewportSizeModel, PaperformatTextfieldA4Vertical.MaximumViewportSize);
            paper = PaperformatFactory.GetPaperformatTextfield(new Size(250, 250));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldA3));
            Assert.AreEqual(paper.ViewportSizeModel, PaperformatTextfieldA3.MaximumViewportSize);
            paper = PaperformatFactory.GetPaperformatTextfield(new Size(385, 287));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldA3));
            Assert.AreEqual(paper.ViewportSizeModel, PaperformatTextfieldA3.MaximumViewportSize);
            paper = PaperformatFactory.GetPaperformatTextfield(new Size(400, 287));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldCustom));
            Assert.AreEqual(paper.ViewportSizeModel, new Size(400, 287));
            paper = PaperformatFactory.GetPaperformatTextfield(new Size(1200, 850));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldCustom));
            Assert.AreEqual(paper.ViewportSizeModel, PaperformatTextfieldCustom.MaximumViewportSize);
        }

        [TestMethod]
        public void IncreaseToNextFormat()
        {
            var paper = PaperformatFactory.GetPaperformatTextfield(new Size(370, 574));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldCustom));
            Assert.AreEqual(new Size(395, 584), paper.ViewportSizeModel);
            paper = PaperformatFactory.GetPaperformatTextfield(new Size(550, 574));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldCustom));
            Assert.AreEqual(new Size(565, 584), paper.ViewportSizeModel);
            paper = PaperformatFactory.GetPaperformatTextfield(new Size(737, 870));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldCustom));
            Assert.AreEqual(new Size(755, 810), paper.ViewportSizeModel);
            paper = PaperformatFactory.GetPaperformatTextfield(new Size(737, 574));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldCustom));
            Assert.AreEqual(new Size(755, 584), paper.ViewportSizeModel);
            paper = PaperformatFactory.GetPaperformatTextfield(new Size(737, 805));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldCustom));
            Assert.AreEqual(new Size(755, 805), paper.ViewportSizeModel);
        }

        [TestMethod]
        public void SizeChange()
        {
            var paper = PaperformatFactory.GetPaperformatTextfield(new Size(250, 160));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldA4Horizontal));
            paper = paper.ChangeSize(new Size(160, 250));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldA4Vertical));
            paper = paper.ChangeSize(new Size(250, 160));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldA4Horizontal));
            paper = paper.ChangeSize(new Size(350, 180));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldA3));
            paper = paper.ChangeSize(new Size(600, 290));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldCustom));
            paper = paper.ChangeSize(new Size(160, 250));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldA4Vertical));
            paper = paper.ChangeSize(new Size(600, 290));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldCustom));
            paper = paper.ChangeSize(new Size(250, 160));
            Assert.IsInstanceOfType(paper, typeof(PaperformatTextfieldA4Horizontal));
        }
    }
}
