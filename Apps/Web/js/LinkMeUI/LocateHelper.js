LinkMeUI.LocateHelper = {        
    
    //DONE: Prototype's .select function does work faster than old getElementsByClassName,
    //but it's still slower than this implementation.
    //Please refer to LinkMeUI.Editor.Stats.TempTest()
    //
    //Review this function's performance when upgrading to new prototype.
    GetElementsByClassName : function(parentElement, className) {
        if(className == null || parentElement == null)
            return null;
        
        var res = null;
        if (document.evaluate) {
            var q = ".//*[contains(concat(' ', @class, ' '), ' " + className + " ')]";
            res = document._getElementsByXPath(q, parentElement);
        } else {
            var classElements = new Array();
            if ( parentElement == null )
                    parentElement = document;
            var tag = '*';
            var els = parentElement.getElementsByTagName(tag);
            var elsLen = els.length;
            var pattern = new RegExp("(^|\\s)"+className+"(\\s|$)");
            for (i = 0, j = 0; i < elsLen; i++) {
                    if ( pattern.test(els[i].className) ) {
                            classElements[j] = $(els[i]);
                            j++;
                    }
            }
            res = $A(classElements); 
        }
        
        return res;
    },

    GetParentControlDivByClassName : function(relTarg, CLASS_NAME) {	    
 	    if(relTarg == null) {
 	        return null;
 	    }

 	    relTarg = $(relTarg);
	 	
	    if(relTarg.hasClassName) {
	        if(relTarg.hasClassName(CLASS_NAME)) {
		        return relTarg;
	        }
	    } else {
	        if(Element.hasClassName(relTarg, CLASS_NAME)) {
		        return relTarg;
	        }		    
	    }		
		
	    var parentNode = relTarg;
	    while(parentNode = $(parentNode.parentNode)) {
		    if(parentNode == parentNode.parentNode) {
			    break;
		    }			
		    if(parentNode.hasClassName && parentNode.hasClassName(CLASS_NAME)) {
			    return parentNode;
		    }			
	    }		
		
		return null;
    },	
	
    GetForAttribute : function(obj) {
	    return obj.getAttribute('for') || obj.getAttribute('htmlFor');
    },

    SetForAttribute : function(obj, val) {
	    if(obj.getAttribute('for') != null) {
		    obj.setAttribute('for', val);
	    } 
	    if(obj.getAttribute('htmlFor') != null) {
		    obj.setAttribute('htmlFor', val);
	    }
    },	
   
    GetSelectedValue : function(obj) {
        return obj.options[obj.selectedIndex].value;
    },    
    
    GetObjectPos : function(obj) {
        //this.WriteLog('Obj id: ' + obj.id + '<br/>');
	    var curleft = curtop = 0;
	    if (obj.offsetParent) {
	        //this.WriteLog('Obj id: ' + obj.id + '. X: ' + obj.offsetLeft + '; Y:' + obj.offsetTop + '<br/>');
		    curleft = obj.offsetLeft;
		    curtop = obj.offsetTop;
		    while (obj = obj.offsetParent) {
		        //this.WriteLog('Obj id: ' + obj.id + '. X: ' + obj.offsetLeft + '; Y:' + obj.offsetTop + '<br/>');
			    curleft += obj.offsetLeft;
			    curtop += obj.offsetTop;
		    }
	    }	
	    return [curleft,curtop];
    },
	
    GetCurrYPos : function() {
        if (document.body && document.body.scrollTop) {
            return document.body.scrollTop;
        }
        if (document.documentElement && document.documentElement.scrollTop) {
            return document.documentElement.scrollTop;
        }
        if (window.pageYOffset) {
            return window.pageYOffset;
        }
        return 0;
    },
    
    IsMSIE : function() {
        if(navigator && navigator.userAgent && navigator.userAgent.indexOf('MSIE') != -1) {
            return true;
        }
        return false;
    }
};