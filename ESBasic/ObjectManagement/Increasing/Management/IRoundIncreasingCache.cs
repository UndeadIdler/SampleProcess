﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ESBasic.ObjectManagement.Increasing.Management
{
    /// <summary>
    /// IRoundIncreasingCache 用于存储当前Round数据的增量缓存
    /// </summary>
    public interface IRoundIncreasingCache<TRoundID, TRoundCache, TObject> where TRoundCache : IRoundCache<TRoundID>
    {
        TRoundID RoundID { get; }

        void AddIncreasement(IList<TObject> list);

        TRoundCache CreateRoundCache();
    }
}
