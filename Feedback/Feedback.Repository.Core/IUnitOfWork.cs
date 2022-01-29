﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Repository.Core
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
