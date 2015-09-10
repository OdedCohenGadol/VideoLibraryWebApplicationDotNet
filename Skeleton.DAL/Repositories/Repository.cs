
using Skeleton.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.DAL
{
    public class Repository : IDisposable
    {
        private bool _disposed = false;
        private readonly VideoEntities _context;
        private readonly bool _isAutoCommit;

        public Repository(bool isAutoCommit = true)
        {
            _context = new VideoEntities();
            _isAutoCommit = isAutoCommit;
        }

        #region private repositories

        private UserRepository _userRepository;
        private VideoRepository _videoRepository;
      

        #endregion

        #region public Properties

        public UserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context, _isAutoCommit)); }
        }

        public VideoRepository VideoRepository 
        {
            get { return _videoRepository ?? (_videoRepository = new VideoRepository(_context, _isAutoCommit)); }
        }

        

        #endregion

        void IDisposable.Dispose()
        {
            if (!_disposed && _context != null)
            {
                _context.Dispose();
                _disposed = true;
            }
        }
    }
}
