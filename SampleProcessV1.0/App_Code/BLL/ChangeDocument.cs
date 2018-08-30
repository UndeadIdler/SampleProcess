using System;
using System.Collections.Generic;

using System.Web;
using Microsoft.Office.Core;

/// <summary>
///ChangeDocument 的摘要说明
/// </summary>
public class ChangeDocument
{
	public ChangeDocument()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    //private static void ConvertWordPDF2(object filename, object savefilename)
    //{
    //    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
    //    Type wordType = wordApp.GetType();
    //    Microsoft.Office.Interop.Word.Documents docs = wordApp.Documents;
    //    Type docsType = docs.GetType();
    //    Microsoft.Office.Interop.Word.Document doc = (Microsoft.Office.Interop.Word.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, (object)docs, new Object[] { filename, true, true });
    //    Type docType = doc.GetType();
    //    docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { savefilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF });
    //    docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, doc, null);
    //    wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, wordApp, null);
    //}
}