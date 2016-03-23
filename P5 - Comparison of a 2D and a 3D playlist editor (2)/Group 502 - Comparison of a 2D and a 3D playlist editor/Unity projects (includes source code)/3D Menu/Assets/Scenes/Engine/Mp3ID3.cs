using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


    public class Mp3ID3
    {

    /*
    In most mp3 files the last 128 bytes are reserved to id3 tags. Titles, artist, albums each have 
    30 bytes reserved. Year have 4 bytes, and genre have 1 byte. The 1 bytes is an index in a list of 
    genres. 
    */
        //The genres is a list premade and common used by ID3
        private static string[] _genres = { "Blues", "Classic Rock", "Country", "Dance", "Disco", "Funk", "Grunge", "Hip-Hop", "Jazz", "Metal", "New Age", "Oldies", "Other", "Pop", "R&B", "Rap", "Reggae", "Rock", "Techno", "Industrial", "Alternative", "Ska", "Death Metal", "Pranks", "Soundtrack", "Euro-Techno", "Ambient", "Trip-Hop", "Vocal", "Jazz+Funk", "Fusion", "Trance", "Classical", "Instrumental", "Acid", "House", "Game", "Sound Clip", "Gospel", "Noise", "AlternRock", "Bass", "Soul", "Punk", "Space", "Meditative", "Instrumental Pop", "Instrumental Rock", "Ethnic", "Gothic", "Darkwave", "Techno-Industrial", "Electronic", "Pop-Folk", "Eurodance", "Dream", "Southern Rock", "Comedy", "Cult", "Gangsta", "Top 40", "Christian Rap", "Pop/Funk", "Jungle", "Native American", "Cabaret", "New Wave", "Psychedelic", "Rave", "Showtunes", "Trailer", "Lo-Fi", "Tribal", "Acid Punk", "Acid Jazz", "Polka", "Retro", "Musical", "Rock & Roll", "Hard Rock", "Folk", "Folk-Rock", "National Folk", "Swing", "Fast Fusion", "Bebob", "Latin", "Revival", "Celtic", "Bluegrass", "Avantgarde", "Gothic Rock", "Progressive Rock", "Psychedelic Rock", "Symphonic Rock", "Slow Rock", "Big Band", "Chorus", "Easy Listening", "Acoustic", "Humour", "Speech", "Chanson", "Opera", "Chamber Music", "Sonata", "Symphony", "Booty Bass", "Primus", "Porn Groove", "Satire", "Slow Jam", "Club", "Tango", "Samba", "Folklore", "Ballad", "Power Ballad", "Rhythmic Soul", "Freestyle", "Duet", "Punk Rock", "Drum Solo", "A capella", "Euro-House", "Dance Hall" };
        //Creates a new list of 128 bytes. 
        private byte[] _id3Bytes = new byte[128];
        //Path of the mp3 file
        private string _mp3FilePath;
        
        //Contructor, takes in a filepath. Will create a new Mp3ID3 object that open a filestream finding
        //the last 128 bytes and reads them to our list of bytes.
        public Mp3ID3(string MP3FilePath)
        {
            using (FileStream fs = File.OpenRead(MP3FilePath))
            {
                fs.Seek(-1* _id3Bytes.Length, SeekOrigin.End);
                fs.Read(_id3Bytes, 0, _id3Bytes.Length); 
                

            }
                _mp3FilePath = MP3FilePath;
        }

        //getByteRange(); Is our homemade reader, it  takes in a start value and an end value
        //It will create a temp list that copies each byte which is returned when the function 
        //is called. 
        private byte[] getByteRange(int start, int length)
        {
            List<byte> returnBytes = new List<byte>(); 
            for (int i = start; i < start + length; i++)
            {
                returnBytes.Add(_id3Bytes[i]); 
            }

            return returnBytes.ToArray(); 
        }
        //The following fuctiong will return a UTF8 encoded string. UTF8 is a character encoding 
        //it's capable of displaying all possible characters. The GetString will take the array 
        //returned from the getByteRange() function and create a string out of them.
        public string Title
        {
                                                   //3, 30  in the Id3 tag is reserved for title                
            get { return Encoding.UTF8.GetString(getByteRange(3, 30)); }
        }

        public string Artist
        {
            get { return Encoding.UTF8.GetString(getByteRange(33, 30)); }

        }

        public string Album
        {
            get { return Encoding.UTF8.GetString(getByteRange(63, 30)); }

        }

        public int Year
        {
            get
            {   //The Parse function will turn the string into a int. 
                try { return int.Parse(Encoding.UTF8.GetString(getByteRange(93, 4))); }
                catch { return -1; }
            }

        }

        public String Comment
        {
            get { return Encoding.UTF8.GetString(getByteRange(97, 28)); }


        }

        public int TrackNumber
        {
            get 
            {
                try
                {   //If the 125th byte is used as a bool. If the zero byte is 0 it means 
                    //a track number is stored in the 126 indexed byte. If it's not 0 the 
                    //next 126th indexed byte will be incalid to read..
                    if ((int)_id3Bytes[125] != 0) return -1;
                    return (int)_id3Bytes[126];
                }catch { return -1; }
            } 
        }
        public string Genre
        {
            get
            {
                try
                {
                    //will return a genre a index 127. A catch is made in case the index is empty.
                    return _genres[(int)_id3Bytes[127]];
                } catch { return ""; }
            }
        }



    }
//}
