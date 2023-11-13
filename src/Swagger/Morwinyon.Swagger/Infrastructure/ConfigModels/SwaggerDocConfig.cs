using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morwinyon.OpenApi;

/// <summary>
/// The Xml Documentation Config for Swagger
/// </summary>
public class SwaggerDocConfig
{
    private string xmlFilePath;

    /// <summary>
    /// If xml documentation is enabled
    /// </summary>
    public bool XmlDocEnabled { get; set; }

    /// <summary>
    /// The xml file path
    /// </summary>
    public string XmlFilePath
    {
        get => xmlFilePath;
        set
        {
            xmlFilePath = value;
            if (!string.IsNullOrEmpty(value))
                XmlDocEnabled = true;
        }
    }
}
