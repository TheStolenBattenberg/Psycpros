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
        private uint iLastID;
        private string sLastType;
        private string sTName;
        //Hasher
        private Psycode.ICheckSum pHasher;

        //Public variables
        public uint iFileNumber = 0;

        /**
         * Constructor
        **/
        public ITReader(string filepath) {
            pHasher = new Psycode.ICheckSum("filehashtable");

            sTName = Path.GetFileNameWithoutExtension(filepath);
                
            try
            {
                //Open the TFile
                pTFile = new BinaryReader(File.Open(filepath, FileMode.Open));

                //Get the number of files
                uint tempFileNumber = pTFile.ReadUInt16();

                pTFileLocation = new uint[tempFileNumber];

                //Loop through and read all files.
                uint i = 0; while (i < tempFileNumber)
                {
                    uint Start = pTFile.ReadUInt16();
                    uint End = pTFile.ReadUInt16();
                    pTFile.BaseStream.Seek(-2, SeekOrigin.Current);

                    //If the file location is not fake, add it to list.
                    if (!((End - Start) <= 0))
                    {
                        uint f = (Start << 16) | (End & 0xFFFF);

                        pTFileLocation[iFileNumber] = f;
                        iFileNumber++;
                    }
                    ++i;
                }
            }catch (IOException e) {
                Console.Write("I/O Error: " + e.Source); Console.WriteLine();
            }
        }
        
        /**
         * Validates file types. TO-DO
        **/
        private void ValidateFile(uint fileStart, out string fileType, uint thisID) {
            //Identify a VB after a VH... ** REPLACE ME **
            if (sLastType == "vh" && iLastID == thisID - 1) {
                fileType = "vb";
                return;
            }

            //First pass. Standard Formats.
            pTFile.BaseStream.Seek(fileStart, SeekOrigin.Begin);
            uint tag1 = pTFile.ReadUInt32();

            switch (tag1) {
                case 0x00000041: //TMD Format
                    fileType = "tmd";
                    return;

                case 0x00000010: //TIM Format
                    fileType = "tim";
                    return;

                case 0x56414270: //VAB Format
                    fileType = "vh";
                    return;

                case 0x53455170: //SEQ Format
                    fileType = "seq";
                    return;
            }

            //Second pass, RTIM.
            uint tag2 = pTFile.ReadUInt32();
            uint tag3 = pTFile.ReadUInt32();
            uint tag4 = pTFile.ReadUInt32();

            //Stop false RTIM identification
            if ((tag1 + tag2) != 0) {
                if ((tag1 + tag2) == (tag3 + tag4)) { //RTIM Format
                    fileType = "rtim";
                    return;
                }
            }

            //Third pass, MO.
            if (pTFile.BaseStream.Length > fileStart + tag3) { //Stop false identification
                pTFile.BaseStream.Seek(fileStart + tag3, SeekOrigin.Begin);
                tag1 = pTFile.ReadUInt32();
                if (tag1 == 0x00000041) { //MO Format
                    fileType = "mo";
                    return;
                }
            }

            //If the file type could not be validated...
            fileType = "wot";
            return;
        }

        /**
         * Extracts a file.
        **/
        public void Extract(uint fileID, string path) {
            //Build file information
            uint   FileStartOffset = 2048 * ((pTFileLocation[fileID] >> 16) & 0xFFFF);
            uint   FileEndOffset = 2048 * ((pTFileLocation[fileID] & 0xFFFF));
            int    FileSize = (int) (FileEndOffset - FileStartOffset);     
                  
            string FileType = "";

            //Get the File Info from hash.
            pTFile.BaseStream.Seek(FileStartOffset, SeekOrigin.Begin);
            byte[] pFile = pTFile.ReadBytes(FileSize);

            //Get Name and directory
            string FileName = pHasher.GetNameFromHash(pFile, fileID.ToString());
            path = path + "\\" + pHasher.GetDirFromHash(pFile, sTName);
            if(!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            //Validate the file type
            ValidateFile(FileStartOffset, out FileType, fileID);

            //Get real file name
            int fileId = 0;
            string file = path + "\\" + FileName + ((fileId > 0) ? "_" + fileId.ToString() : "")
            + "." + FileType;
            while (File.Exists(file)) {
                file = path + "\\" + FileName + ((fileId > 0) ? "_" + fileId.ToString() : "") + "." + FileType;
                fileId++;
            }

            //Extract the file
            Console.WriteLine("Extracting " + file + "...");
            BinaryWriter fOut = new BinaryWriter(File.Open(file, FileMode.CreateNew));

            pTFile.BaseStream.Seek(FileStartOffset, SeekOrigin.Begin);
            
            pTFile.BaseStream.CopyTo(fOut.BaseStream, FileSize);
            fOut.BaseStream.SetLength(FileSize);
            fOut.Close();

            iLastID = fileID;
            sLastType = FileType;
        }

        public void Close() {
            pHasher.SaveHashFile("filehashtable");
        }
    }
}
