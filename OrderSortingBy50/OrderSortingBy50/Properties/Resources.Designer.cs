﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrderSortingBy50.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OrderSortingBy50.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找 System.Drawing.Bitmap 类型的本地化资源。
        /// </summary>
        internal static System.Drawing.Bitmap delete {
            get {
                object obj = ResourceManager.GetObject("delete", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查找类似 欢迎您：{0} 的本地化字符串。
        /// </summary>
        internal static string LoginSuccess {
            get {
                return ResourceManager.GetString("LoginSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 阻挡错误 的本地化字符串。
        /// </summary>
        internal static string PleaseSolveBlockError {
            get {
                return ResourceManager.GetString("PleaseSolveBlockError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 请先扫描投递出错订单：{0} 的本地化字符串。
        /// </summary>
        internal static string PleaseSolvePutError {
            get {
                return ResourceManager.GetString("PleaseSolvePutError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 存在未投递的产品 的本地化字符串。
        /// </summary>
        internal static string PleaseWait {
            get {
                return ResourceManager.GetString("PleaseWait", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 在波次：{0}中未找到产品：{1} 的本地化字符串。
        /// </summary>
        internal static string ProductError {
            get {
                return ResourceManager.GetString("ProductError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 投递出错已解除：{0} 的本地化字符串。
        /// </summary>
        internal static string RemovePutError {
            get {
                return ResourceManager.GetString("RemovePutError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 请先投递产品:{0} 的本地化字符串。
        /// </summary>
        internal static string WaitPut {
            get {
                return ResourceManager.GetString("WaitPut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该波次已完成：{0} 的本地化字符串。
        /// </summary>
        internal static string WaveCompleteError {
            get {
                return ResourceManager.GetString("WaveCompleteError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 未找到波次：{0} 的本地化字符串。
        /// </summary>
        internal static string WaveError {
            get {
                return ResourceManager.GetString("WaveError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 波次正在工作中：{0} 的本地化字符串。
        /// </summary>
        internal static string WaveingError {
            get {
                return ResourceManager.GetString("WaveingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 扫描波次成功：{0} 的本地化字符串。
        /// </summary>
        internal static string WaveSuccess {
            get {
                return ResourceManager.GetString("WaveSuccess", resourceCulture);
            }
        }
    }
}
