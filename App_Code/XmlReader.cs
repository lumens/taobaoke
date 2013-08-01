using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
///XmlReader 的摘要说明
/// </summary>
public class XmlReader
{
	public XmlReader()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 读取配置文件
    /// </summary>
    /// <param name="filepath"></param>
    /// <param name="nodename"></param>
    /// <returns></returns>
    public static string GetConfig(string filepath, string nodename) 
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filepath); //加载xml  
        XmlNodeList list  = xmlDoc.GetElementsByTagName("add"); //xml节点的路径 
        //循环遍历节点  
        foreach (XmlElement node in list) 
        {
            if (node.Attributes["key"].Value.Equals(nodename)) 
            {
                return node.Attributes["value"].Value;
            }
        }
        return null;
    }
}