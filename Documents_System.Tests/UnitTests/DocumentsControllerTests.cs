using Documents_System.Controllers;
using Documents_Sytem.Domain.Abstract;
using Documents_Sytem.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq.Expressions;
using Documents_System.Models;
using System.Web;
using Documents_System.Infrastructure;
using System.IO;

namespace Documents_System.Tests.UnitTests
{
    [TestClass]
    public class DocumentsControllerTests
    {
        [TestMethod]
        public void Index_DetailsView()
        {
            //Arrange
            Mock<IRepository<Document>> mock = new Mock<IRepository<Document>>();

            var target = new DocumentsController(mock.Object);

            //Act
            var result = target.Index(null) as ViewResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Create_DetailsView()
        {
            //Arrange
            Mock<IRepository<Document>> mock = new Mock<IRepository<Document>>();

            var target = new DocumentsController(mock.Object);

            //Act
            var result = target.Create() as ViewResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Index_Contents_All_Documents()
        {
            //Arrange
            Mock<IRepository<Document>> mock = new Mock<IRepository<Document>>();

            ICollection<Document> documents = new List<Document> {
                 new Document { Id = 1, Name = "Doc01", Category = "movies" },
                new Document { Id = 2, Name = "02Doc", Category = "news" },
                new Document { Id = 3, Name = "Doc03", Category = "sports" },
            };

            mock.Setup(d => d.All()).Returns(documents.AsQueryable());

            var target = new DocumentsController(mock.Object);

            //Act
            var action = target.Index("all") as ViewResult;
            Document[] result = ((DocumentViewModel)action.ViewData.Model).Documents.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 3);
            Assert.IsTrue(result[0].Name == "Doc01" && result[0].Category == "movies");
            Assert.IsTrue(result[1].Name == "02Doc" && result[1].Category == "news");
            Assert.IsTrue(result[2].Name == "Doc03" && result[2].Category == "sports");
        }

        [TestMethod]
        public void Index_Contents_Filtered_Documents()
        {
            //Arrange
            Mock<IRepository<Document>> mock = new Mock<IRepository<Document>>();

            ICollection<Document> documents = new List<Document> {
                 new Document { Id = 1, Name = "Doc01", Category = "movies" },
                new Document { Id = 2, Name = "02Doc", Category = "news" },
                new Document { Id = 3, Name = "Doc03", Category = "sports" },
                new Document { Id = 3, Name = "03Doc", Category = "sports" }
            };

            mock.Setup(d => d.All()).Returns(documents.AsQueryable());

            var target = new DocumentsController(mock.Object);

            //Act
            var action = target.Index("d") as ViewResult;
            Document[] result = ((DocumentViewModel)action.ViewData.Model).Documents.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "Doc01" && result[0].Category == "movies");
            Assert.IsTrue(result[1].Name == "Doc03" && result[1].Category == "sports");
        }
    }
}
