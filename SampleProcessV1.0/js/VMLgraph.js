var xo=0;
var yo=0;
var ox=80;
var oy=20;
var dx=0;
var dy=0;
var drawKey = false;
var itemID = 0;
var ShapeItemNum = 0;
var ShapeItemX = 0;
var ShapeItemY = 0;
var CurveItemNum = 0;
var NodeDelete = false;
var ToolBarNum = 2; // 预置的工具编号
var gradientX = -1;

function cursor(k) {
xo = event.clientX - ox;
yo = event.clientY - oy;
if(k && xo>=0 && yo>=0)
oxy.innerHTML = xo+","+yo;
else
oxy.innerHTML = "";
if(drawKey) {
paint();
view.innerHTML = tree(canvas.documentElement,0);
}
}

function setOverColor(v) {
if(! NodeDelete) return;
v.myColor = v.strokecolor;
if(v.strokecolor == "red")
v.strokecolor='#000000';
else
v.strokecolor='#ff0000';
}
function setOutColor(v) {
if(! NodeDelete) return;
v.strokecolor = v.myColor;
view.innerHTML = tree(canvas.documentElement,0);
}
function deleteNode(v) {
if(! NodeDelete) return;
var id = v.id;
for(i=0;i<canvas.selectNodes("/*//*").length;i++) {
var node = canvas.selectNodes("/*//*");
if(node.getAttribute("id") == id) {
canvas.documentElement.childNodes[0].removeChild(node);
view.innerHTML = tree(canvas.documentElement,0);
return;
}
}
}

function setElement(node) {
node.setAttribute("id") = itemID;
node.setAttribute("myColor") = "#"; 
node.setAttribute("onMouseOver") = "setOverColor(this)";
node.setAttribute("onMouseOut") = "setOutColor(this)";
node.setAttribute("onClick") = "deleteNode(this)";

var subobjField = canvas.createElement("v:stroke");
subobjField.setAttribute("color") = color1.fillcolor;
subobjField.setAttribute("dashstyle") = dashstyle.dashstyle;
node.appendChild(subobjField);
if(textbox.style.visibility == "visible" && txt.value.length) {
var subobjField = canvas.createElement("v:path");
subobjField.setAttribute("textpathok") = "true";
node.appendChild(subobjField);
var subobjField = canvas.createElement("v:textpath");
subobjField.setAttribute("on") = "true";
subobjField.setAttribute("string") = txt.value;
subobjField.setAttribute("style") = "font:normal normal normal 16pt 'Arial Black'";
node.appendChild(subobjField);
}
canvas.documentElement.childNodes[0].appendChild(node);
}

function mouse_down() {
drawKey = true;
dx = xo;
dy = yo;
itemID++;
if(ToolBarNum != 7) ShapeItemNum = 0;
switch(ToolBarNum) {
case 3:
var objField = canvas.createElement("v:line");
objField.setAttribute("from") = xo+","+yo;
objField.setAttribute("to") = xo+","+yo;
return setElement(objField);
case 4:
if(CurveItemNum == 0) {
CurveItemNum = 1;
var objField = canvas.createElement("v:curve");
objField.setAttribute("from") = xo+","+yo;
objField.setAttribute("to") = xo+","+yo;
objField.setAttribute("control1") = xo+","+yo;
objField.setAttribute("control2") = xo+","+yo;
var subobjField = canvas.createElement("v:fill");
subobjField.setAttribute("opacity") = 0;
objField.appendChild(subobjField);
return setElement(objField);
}
return;
case 9:
var objField = canvas.createElement("v:polyline");
objField.setAttribute("points") = xo+","+yo+" "+xo+","+yo;
var subobjField = canvas.createElement("v:fill");
subobjField.setAttribute("opacity") = 0;
objField.appendChild(subobjField);
return setElement(objField);
case 7:
if(ShapeItemNum == 0) {
var objField = canvas.createElement("v:shape");
objField.setAttribute("style") = "width:500; height:309";
objField.setAttribute("path") = "m "+xo+","+yo+" l "+xo+","+yo;
ShapeItemX = xo;
ShapeItemY = yo;
}else {
objField = canvas.documentElement.childNodes[0].lastChild;
objField.setAttribute("path") = objField.getAttribute("path") + " "+xo+","+yo;
return;
}
ShapeItemNum++;
break;
case 5:
var objField = canvas.createElement("v:rect");
break;
case 6:
var objField = canvas.createElement("v:roundrect");
objField.setAttribute("arcsize") = 0.2;
break;
case 8:
var objField = canvas.createElement("v:oval");
break;
case 10:
s = "";
s = tree(canvas.documentElement,1);
view.innerHTML = s; 
return;
defaule:
drawKey = false;
return;
}
if(objField) {
if(ToolBarNum != 7)
objField.setAttribute("style") = "left:"+xo+"; top:"+yo+"; width:0; height:0;";
var subobjField = canvas.createElement("v:fill");
subobjField.setAttribute("opacity") = gradientBar.opacity;
subobjField.setAttribute("angle") = gradientBar.angle;
subobjField.setAttribute("type") = gradientBar.type;
subobjField.setAttribute("color") = gradientBar.color.value;
subobjField.setAttribute("color2") = gradientBar.color2.value;
subobjField.setAttribute("colors") = gradientBar.colors.value;
subobjField.setAttribute("focusposition") = gradientBar.focusposition;
objField.appendChild(subobjField);
return setElement(objField);
}
return;
}

function mouse_up() {
drawKey = false;
if(CurveItemNum > 0) CurveItemNum++;
if(CurveItemNum > 3) CurveItemNum = 0;
if(ToolBarNum == 7) {
if(Math.abs(xo - ShapeItemX) < 2 && Math.abs(yo - ShapeItemY) < 2) {
ShapeItemNum = 0;
Element = canvas.documentElement.childNodes[0].lastChild;
var regerp = / [0-9]+,[0-9]+$/
var str = Element.getAttribute("path");
Element.setAttribute("path") = str.replace(regerp," x e");
view.innerHTML = tree(canvas.documentElement,0);
}
}
}

function paint() {
Element = canvas.documentElement.childNodes[0].lastChild;
var x0,y0,x1,y1;
x0 = Math.min(dx,xo);
y0 = Math.min(dy,yo);
x1 = Math.max(dx,xo);
y1 = Math.max(dy,yo);
var box = "left:"+x0+"; top:"+y0+"; width:"+(x1-x0)+"; height:"+(y1-y0)+";";
switch(ToolBarNum) {
case 4:
if(CurveItemNum ==2) {
Element.setAttribute("control1") = xo+","+yo;
Element.setAttribute("control2") = Element.getAttribute("to");
break;
}
if(CurveItemNum ==3) {
Element.setAttribute("control2") = xo+","+yo;
break;
}
case 3:
Element.setAttribute("to") = xo+","+yo;
break;
case 7:
var regerp = /[0-9]+,[0-9]+$/
var str = Element.getAttribute("path");
Element.setAttribute("path") = str.replace(regerp,xo+","+yo);
break;
case 5:
case 6:
case 8:
var regerp = /left.+height:[0-9]+;/
var str = Element.getAttribute("style");
Element.setAttribute("style") = str.replace(regerp,box);
break;
case 9:
var regerp = / [0-9]+,[0-9]+$/
var str = Element.getAttribute("points");
Element.setAttribute("points") = str+" "+xo+","+yo;
break;
defaule:
break;
}

}

function init() {
tool_box_refresh(); // 工具栏初始
view.innerHTML = tree(canvas.documentElement); // 绘图区初始
color.innerHTML = tree(tools.selectNodes("*/colorbar")[0]); // 颜色选择初始
linebox.innerHTML = tree(tools.selectNodes("*/linebox")[0]); // 线型选择初始
gradientBox.innerHTML = tree(tools.selectNodes("*/gradient")[0]); // 充填选择初始
}

// 绘制工具栏
function tool_box_refresh() {
var buffer = "";
var i;
for(i=0;i<tools.selectNodes("*/toolbar").length;i++) {
var node = tools.selectNodes("*/toolbar");
var id = node.getAttribute("id");
node.childNodes[0].setAttribute("onClick") = "tool_box_select("+id+",this.title)";
var node1 = node.selectNodes("*/v:rect")[0];
if(id == ToolBarNum) {
node1.setAttribute("fillcolor") = "#ffcccc"
node1.setAttribute("strokecolor") = "#ff0000"
}else {
node1.setAttribute("fillcolor") = "#ffffff"
node1.setAttribute("strokecolor") = "#000000"
}
buffer += tree(node.childNodes[0]);
}
toolbox.innerHTML = buffer;
}

// 工具选择
function tool_box_select(v,t) {
var key = ToolBarNum;
ToolBarNum = v;
tool_box_refresh();
hooke();
if(v == 7) {
if(key == 7 && ShapeItemNum > 0) {
Element = canvas.documentElement.childNodes[0].lastChild;
var str = Element.getAttribute("path");
Element.setAttribute("path") = str + " x e";
view.innerHTML = tree(canvas.documentElement,0);
ShapeItemNum = 0;
}
}
if(v == 10)
if(textbox.style.visibility == "hidden")
textbox.style.visibility = "visible";
else
textbox.style.visibility = "hidden";
NodeDelete = false;
if(v == 1) {
NodeDelete = true;
view.innerHTML = tree(canvas.documentElement,0);
}
}

// 颜色选择
//var setcolorkey = color1;
function setcolor(c) {
var setcolorkey = color1;
setcolorkey.fillcolor = c;
}

function gradientColor(v) {
v.fillcolor = color1.fillcolor;
gradientRefresh();
return;
var m = tools.documentElement.selectNodes("/*/gradient//v:shape").length;
var node = tools.documentElement.selectNodes("/*/gradient//v:shape");
for(i=0;i<m;i++) {
if(node.getAttribute("id") == v.id)
node.setAttribute("fillcolor") = color1.fillcolor;
}
gradientRefresh();
}
function gradientPoint(v) {
if(gradientX < 0)
gradientX = xo - 508 - parseInt(v.style.left);
n = xo - 508 - gradientX;
if(n < 8) n = 8;
if(n > 108) n = 108;
v.style.left = n;
gradientRefresh();
}
function anglePoint(v) {
angle.style.left = Math.floor((xo-516)/25)*25+8;
gradientRefresh();
}
function opacityPoint(v) {
opacity.style.left = Math.floor((xo-516)/25)*25+8;
gradientRefresh();
}
function settype(v) {
if(v.style.borderColor == "black")
v.style.borderColor = "red";
else
v.style.borderColor = "black";
gradientRefresh();
}
function setGradientX() {
gradientX = -1;
}

function gradientRefresh() {
var m = (parseInt(gradient4.style.left)-parseInt(gradient1.style.left));
var n1 = (parseInt(gradient2.style.left)-parseInt(gradient1.style.left))/m*100;
var n2 = (parseInt(gradient3.style.left)-parseInt(gradient1.style.left))/m*100;
gradientBar.color.value = gradient1.fillcolor;
gradientBar.color2.value = gradient4.fillcolor;
if(type3.style.borderColor == "black")
gradientBar.colors.value = ",";
else
gradientBar.colors.value = n1 + "% " + gradient2.fillcolor + "," + n2 + "% " + gradient3.fillcolor;
if(type1.style.borderColor == "black")
gradientBar.type = "solid";
else
gradientBar.type = "gradient";
if(type2.style.borderColor == "red")
gradientBar.type = "gradientradial";
n1 = (parseInt(focus1.style.left)-8)/m*100;
n2 = (parseInt(focus2.style.left)-8)/m*100;
gradientBar.focusposition.value = n1 + "%," + n2 + "%";
gradientBar.angle = (parseInt(angle.style.left)-8) * 3.6;
gradientBar.opacity = (parseInt(opacity.style.left)-8)/m
}
