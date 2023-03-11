using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest : TestBase
    {
        private const string BAD_FILE_NAME = @"C:\Bogus.exe";
        //private const string GOOD_FILE_NAME = @"C:\Windows\Regedit.exe";

        
        [TestMethod]
        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();
            if (!string.IsNullOrEmpty(_GoodFileName))
            {
                // Create the 'Good' file.
                File.AppendAllText(_GoodFileName, "Some Text");
            }
            TestContext.WriteLine(@"Checking File " + _GoodFileName );

            fromCall = fp.FileExists(_GoodFileName);
            // Delete file
            if (File.Exists(_GoodFileName))
            {
                File.Delete(_GoodFileName);
            }
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine(@"Checking for null file " + BAD_FILE_NAME);


            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_UsingAttribute()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }

        [TestMethod]
        public void FileNameNullOrEmpty_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch (ArgumentNullException)
            {

                return;
            }

            Assert.Fail("Call to FileExists() did not throw an ArgumentNullException");
        }
    }
}
