// Copyright (c) Martin Eden http://stackoverflow.com/users/777939/martin-eden.  
// MIT License http://opensource.org/licenses/MIT.
// 
// Author: Nicholas Rogoff
// Created: 2016 - 12 - 26
// 
// Project: hms.ImageProcessingConsole
// Filename: JpegMetadataAdapter.cs

using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace hms.ImageProcessingConsole
{
    public class JpegMetadataAdapter : IDisposable
    {
        private readonly string _path;
        private BitmapFrame _frame;
        public BitmapMetadata Metadata;

        public JpegMetadataAdapter(string path)
        {
            _path = path;
            _frame = getBitmapFrame(path);
            Metadata = (BitmapMetadata) _frame.Metadata.Clone();
        }

        public void Save()
        {
            SaveAs(_path);
        }

        public void SaveAs(string path)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(_frame, _frame.Thumbnail, Metadata, _frame.ColorContexts));
            using (Stream stream = File.Open(path, FileMode.Create, FileAccess.ReadWrite))
            {
                encoder.Save(stream);
            }
        }

        private BitmapFrame getBitmapFrame(string path)
        {
            BitmapDecoder decoder = null;
            using (Stream stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                decoder = new JpegBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            }
            return decoder.Frames[0];
        }

        #region IDisposable

        //indicates if dispose has already been called
        private bool _disposed = false;

        //Finalize method for the object, will call Dispose for us
        //to clean up the resources if the user has not called it
        ~JpegMetadataAdapter()
        {
            //Indicate that the GC called Dispose, not the user
            Dispose(false);
        }

        //This is the public method, it will HOPEFULLY but
        //not always be called by users of the class
        public void Dispose()
        {
            //indicate this was NOT called by the Garbage collector
            Dispose(true);

            //Now we have disposed of all our resources, the GC does not
            //need to do anything, stop the finalizer being called
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            //Check to see if we have already disposed the object
            //this is necessary because we should be able to call
            //Dispose multiple times without throwing an error
            if (!_disposed)
            {
                if (disposing)
                {
                    //clean up managed resources
                    if (_frame != null)
                    {
                        _frame = null;
                    }
                }

                //clear up any unmanaged resources - this is safe to
                //put outside the disposing check because if the user
                //called dispose we want to also clean up unmanaged
                //resources, if the GC called Dispose then we only
                //want to clean up managed resources
            }


            #endregion
        }
    }
}