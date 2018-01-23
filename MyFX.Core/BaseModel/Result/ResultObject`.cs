﻿using System;

namespace MyFX.Core.BaseModel.Result
{
    /// <summary>
    /// 相应信息包装类
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    [Serializable]
    public class ResultObject<TBody> : ResultObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultObject()
            : base()
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="retBody">要返回的业务数据</param>
        public ResultObject(TBody retBody) :
            this()
        {
           this.retBody = retBody;
        }

        /// <summary>
        /// 返回的业务数据
        /// </summary>
        public TBody retBody { get; set; }

    }
}