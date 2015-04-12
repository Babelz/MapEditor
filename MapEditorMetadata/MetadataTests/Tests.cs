using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoidenMetadata;
using System.Diagnostics;

namespace MetadataTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        [ExpectedException(typeof(MetadataSystemException), "Invalid field name was allowed.")]
        public void WillCrashOnInvalidField()
        {
            MetadataObject o = new MetadataObject("object",
                new MetadataField[]
                {
                    new MetadataField("num", 10)
                });

            o.GetFieldValue<int>("Num");
        }

        [TestMethod]
        [ExpectedException(typeof(MetadataSystemException), "Invalid type was allowed.")]
        public void WillCrashOnTypeMismatch()
        {
            MetadataObject o = new MetadataObject("object",
                new MetadataField[]
                {
                    new MetadataField("num", 10)
                });

            o.GetFieldValue<float>("num");
        }

        [TestMethod]
        public void WillNotCrashOnValidField()
        {
            MetadataObject o = new MetadataObject("object",
                new MetadataField[]
                {
                    new MetadataField("num", 10)
                });

            try
            {
                o.GetFieldValue<int>("num");
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void WillNotCrashOnValidType()
        {
            MetadataObject o = new MetadataObject("object",
                new MetadataField[]
                {
                    new MetadataField("num", 10)
                });

            try
            {
                o.GetFieldValue<int>("num");
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MetadataSystemException), "Invalid type was allowed.")]
        public void ThrowOnNonUniqueName()
        {
            MetadataObject o = new MetadataObject("object",
                new MetadataField[]
                {
                    new MetadataField("num", 10)
                });

            MetadataObject a = new MetadataObject(o.Name, o);
        }

        [TestMethod]
        public void DoesNotThrowOnUniqueName()
        {
            MetadataObject o = new MetadataObject("object",
                     new MetadataField[]
                {
                    new MetadataField("num", 10)
                });

            MetadataObject a = new MetadataObject("object2", o);
        }

        [TestMethod]
        [ExpectedException(typeof(MetadataSystemException), "Invalid type was allowed.")]
        public void ThrowsOnTypeMismatch()
        {
            MetadataObject o = new MetadataObject("object",
         new MetadataField[]
                {
                    new MetadataField("num", 10)
                });

            o.SetFieldValue("num", false);
        }

        [TestMethod]
        [ExpectedException(typeof(MetadataSystemException), "Invalid type was allowed.")]
        public void ThrowsOnInvalidField()
        {
            MetadataObject o = new MetadataObject("object",
         new MetadataField[]
                {
                    new MetadataField("num", 10)
                });

            o.SetFieldValue("numm", 25);
        }

        [TestMethod]
        public void CanSetField()
        {
            MetadataObject o = new MetadataObject("object",
         new MetadataField[]
                {
                    new MetadataField("num", 10)
                });

            o.SetFieldValue("num", 20);

            Assert.AreEqual(o.GetFieldValue<int>("num"), 20);
        }
    }
}
