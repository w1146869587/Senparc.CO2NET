﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/Senparc/Senparc.CO2NET/blob/master/LICENSE

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：ServerUtility.cs
    文件功能描述：服务器工具类
    
    
    创建标识：Senparc - 20180819

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Senparc.CO2NET.Utilities
{
    /// <summary>
    /// 服务器工具类
    /// </summary>
    public class ServerUtility
    {
        private static string _appDomainAppPath;

        /// <summary>
        /// dll 项目根目录
        /// </summary>
        public static string AppDomainAppPath
        {
            get
            {
                if (_appDomainAppPath == null)
                {
#if NET35 || NET40 || NET45
                    _appDomainAppPath = HttpRuntime.AppDomainAppPath;
#else
                    _appDomainAppPath = AppContext.BaseDirectory; //dll所在目录：;
#endif
                }
                return _appDomainAppPath;
            }
            set
            {
                _appDomainAppPath = value;
#if NETSTANDARD1_6 || NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
                if (!_appDomainAppPath.EndsWith("/"))
                {
                    _appDomainAppPath += "/";
                }
#endif
            }
        }

        /// <summary>
        /// 获取相对于网站根目录的文件路径
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public static string ContentRootMapPath(string virtualPath)
        {
            if (virtualPath == null)
            {
                return "";
            }
            else
            {
                if (!Config.RootDictionaryPath.EndsWith("/") || Config.RootDictionaryPath.EndsWith("\\"))
                {
                    Config.RootDictionaryPath += "\\";
                }

                if (virtualPath.StartsWith("~/"))
                {
                    return virtualPath.Replace("~/", Config.RootDictionaryPath).Replace("/", "\\");
                }
                else
                {
                    return Path.Combine(Config.RootDictionaryPath, virtualPath);
                }
            }
        }

        /// <summary>
        /// 获取相对于dll目录的文件绝对路径
        /// </summary>
        /// <param name="virtualPath">虚拟路径，如~/App_Data/</param>
        /// <returns></returns>
        public static string DllMapPath(string virtualPath)
        {
            if (virtualPath == null)
            {
                return "";
            }
            else if (virtualPath.StartsWith("~/"))
            {
                return virtualPath.Replace("~/", AppDomainAppPath).Replace("/", "\\");
            }
            else
            {
                return Path.Combine(AppDomainAppPath, virtualPath);
            }
        }

    }
}
