﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce.Web.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class GeneralResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GeneralResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Ecommerce.Web.Core.Resources.GeneralResource", typeof(GeneralResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This email isn&apos;t valid..!!.
        /// </summary>
        public static string EmailMSGERROR {
            get {
                return ResourceManager.GetString("EmailMSGERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password is not matching..!!.
        /// </summary>
        public static string MatchingMSGERROR {
            get {
                return ResourceManager.GetString("MatchingMSGERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password must be more than 5 character..!!.
        /// </summary>
        public static string PasswordLENMSGERROR {
            get {
                return ResourceManager.GetString("PasswordLENMSGERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} must be more than {0}..!!.
        /// </summary>
        public static string RangeV1MSGERROR {
            get {
                return ResourceManager.GetString("RangeV1MSGERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} must be between {1} and {2}.
        /// </summary>
        public static string RangeV2MSGERROR {
            get {
                return ResourceManager.GetString("RangeV2MSGERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} must be required..!!.
        /// </summary>
        public static string RequiredMSGERROR {
            get {
                return ResourceManager.GetString("RequiredMSGERROR", resourceCulture);
            }
        }
    }
}
