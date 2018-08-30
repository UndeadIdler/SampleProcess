function saveXML()
{
var xmlHTTP = new ActiveXObject("Microsoft.XMLHTTP");
xmlHTTP.open("POST","server.php",false); // 使用ASP时用server.asp
xmlHTTP.setRequestHeader("Contrn-type","text/xml");
xmlHTTP.setRequestHeader("Contrn-charset","gb2312");

xmlHTTP.send(tree(canvas.documentElement));
var s = xmlHTTP.responseText;
minview.innerHTML = s.replace(/WIDTH:500;HEIGHT:300/,"WIDTH:120;HEIGHT:72")
if(xmlHTTP.responseText.indexOf("Error:")!=-1) {
alert(xmlHTTP.responseText);
}
}

// 遍历xml对象，解析xml的核心函数集
function tree(Element,debug) {
var buffer = "";
var node = "";
if(Element.nodeType != 3) {
node = Element;
buffer += onElement(Element,debug);
}
if(Element.nodeType == 3)
buffer += onData(Element);
if(Element.hasChildNodes) {
for(var i=0;i<Element.childNodes.length;i++) {
buffer += tree(Element.childNodes(i),debug);
}
}
if(node)
buffer += endElement(node,debug);
return buffer;
}

/***** 以下三个函数请根据需要自行修改 *****/
// 遍历xml对象--节点开始
function onElement(Element,debug) {
var buffer = (debug ? "<" : "<") + Element.nodeName;
n = Element.attributes.length
if(n>0) { // 若该节点有参数
for(var i=0;i<n;i++)
buffer += ' ' + Element.attributes(i).name + '=\"' + Element.attributes(i).value + '"';
}
buffer += debug ? ">" : ">";
return buffer;
}

// 遍历xml对象--节点结束
function endElement(Element,debug) {
return (debug ? "</" : "</") + Element.nodeName + (debug ? "><br>" : ">");
}

// 遍历xml对象--节点数据
function onData(Element) {
return Element.nodeValue
}

