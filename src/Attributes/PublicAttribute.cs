using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight.NullLink.Attributes;
/// <summary>
/// Attribute that marks grains / methods as “skip-auth”
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method,
                Inherited = true, AllowMultiple = false)]
public sealed class PublicAttribute : Attribute { }