using System;
using AutoMapper;

namespace Core.Infrastructure.Mapper
{
    public interface IMapperConfiguration
    {
        /// <summary>
        ///     排序
        /// </summary>
        int Order { get; }
        /// <summary>
        /// 得到配置
        /// </summary>
        /// <returns></returns>
        Action<IMapperConfigurationExpression> GetConfiguration();
    }
}