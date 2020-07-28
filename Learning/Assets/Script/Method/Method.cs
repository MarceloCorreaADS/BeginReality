using UnityEngine;
using System.Collections;
using System;
using System.Text;

[Serializable]
public class Method {
	public string name;
	public string returnType;
	public string attributes;
	public string method;
	public string methodFormatted;

	public Method(string name, string returnType, string attributes, string method) {
		string methodFormatted;
		this.name = name;
		this.returnType = returnType;
		this.attributes = attributes;
		this.method = method;
		StringBuilder str = new StringBuilder();

		str.Append("private " + returnType + " " + name);
		if (attributes != "")
			str.Append(" (" + attributes + ") {\n");
		else
			str.Append("(){\n");
		str.Append(method);
		str.Append("\n}");

		this.methodFormatted = str.ToString();
	}

	public string MethodFinal() {
		StringBuilder str = new StringBuilder();

		str.Append("private " + returnType + " " + name);
		if (attributes != "")
			str.Append(" (" + attributes + ") {\n");
		else
			str.Append("(){\n");
		str.Append("try {\n");
		str.Append(method);
		str.Append("\n} catch(System.Exception e) {\n");
		str.Append("ConsoleReport.LogReport(e.Message);\n");
		str.Append("}\n");
		str.Append("\n}");

		return str.ToString();
	}
}
