// JScript 文件

function OnlyNum()//限制输入为数字
{
   if(event.keyCode >47 && event.keyCode < 58)
    {
     event.returnValue =true;
    }
    else
    {
    event.returnValue=false;
    }
}

function OnlyNumDot()//限制输入为数字和小数点
{
   if(event.keyCode >47 && event.keyCode < 58||event.keyCode==46)
    {
     event.returnValue =true;
    }
    else
    {
    event.returnValue=false;
    }
}

function OnlyNumDashes()//限制输入为数字和负号
{
    if(event.keyCode>47&&event.keyCode<58||event.keyCode==45)
    {
        event.returnValue=true;
    }
    else
    {
        event.returnValue=false;
    }
}

function test_email(strEmail) 
{ 
  var myReg = /^[_a-z0-9]+@([_a-z0-9]+\.)+[a-z0-9]{2,3}$/; 
  if(!myReg.test(strEmail)) 
  {
    alert('请填写信息');
    return false;
  }
    else
    {
    return true;
    }

 }
 function xIsNull(strText)
 {
    var myReg=/^S+$/;
    if(!myReg.test(strText))
    {
        alert('不能为空！');
        return false;
    }
    else 
    {
        return true;
    }
 } 