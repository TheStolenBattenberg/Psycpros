using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Psycpros.Reader {
    //T Reading class
    class ITReader {

        //Private Variables
        private BinaryReader pTFile = null;
        private uint[] pTFileLocation;

        //Protected variables
        public uint iFileNumber = 0;
       
    
        /**
         * Constructor
        **/
        public ITReader(string filepath) {
            //Open the TFile
            pTFile = new BinaryReader(File.Open(filepath, FileMode.Open));

            //Get the number of files
            uint tempFileNumber = pTFile.ReadUInt16();

            pTFileLocation = new uint[tempFileNumber];

            //Loop through and read all files.
            uint i = 0; while(i < tempFileNumber) {              
                uint Start = pTFile.ReadUInt16();
                uint End = pTFile.ReadUInt16();
                pTFile.BaseStream.Seek(-2, SeekOrigin.Current);

                //If the file location is not fake, add it to list.
                if(!((End - Start) <= 0)) {
                    uint f = (Start << 16) | (End & 0xFFFF);

                    pTFileLocation[iFileNumber] = f;
                    iFileNumber++;           
                }
                ++i;
            }
        }
        
        /**
         * Validates file types. TO-DO
        **/
        private void ValidateFile(uint fileID, out string fileType) {
            fileType = "ukn";
            return;
        }

        /**
         * Extracts a file.
        **/
        public void Extract(uint fileID, string path) {
            //Build file information
            string FileType = "";
            ValidateFile(fileID, out FileType);

            uint   FileStartOffset = 2048 * ((pTFileLocation[fileID] >> 16) & 0xFFFF);
            uint   FileEndOffset = 2048 * ((pTFileLocation[fileID] & 0xFFFF));
            int    FileSize = (int) (FileEndOffset - FileStartOffset);
            string FileName = "File_" + fileID.ToString() + "." + FileType;

            // Extract the file.
            Console.Write("Extracting " + FileName); Console.WriteLine();

            BinaryWriter fOut = new BinaryWriter(File.Open(path + "\\" + FileName, FileMode.CreateNew));
            pTFile.BaseStream.Seek(FileStartOffset, SeekOrigin.Begin);

            pTFile.BaseStream.CopyTo(fOut.BaseStream, FileSize);
            fOut.BaseStream.SetLength(FileSize);
            fOut.Close();
        }
    }
}
