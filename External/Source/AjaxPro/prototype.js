/*
    27.03.2008, AS: This file have been modified to exclude conflicting with Prototype (http://www.prototypejs.org/)
    functions. Please see revision history if you need those removed ones.
*/

Object.extend(Array.prototype, {
	addRange: function(items) {
		if(items.length > 0) {
			for(var i=0; i<items.length; i++) {
				this.push(items[i]);
			}
		}
	},
	shift: function() {
		if(this.length == 0) { return null; }
		var o = this[0];
		for(var i=0; i<this.length-1; i++) {
			this[i] = this[i + 1];
		}
		this.length--;
		return o;
	}
}, false);

Object.extend(String.prototype, {
	trimLeft: function() {
		return this.replace(/^\s*/,"");
	},
	trimRight: function() {
		return this.replace(/\s*$/,"");
	},
	trim: function() {
		return this.trimRight().trimLeft();
	}
}, false);

Object.extend(String, {
	format: function(s) {
		for(var i=1; i<arguments.length; i++) {
			s = s.replace("{" + (i -1) + "}", arguments[i]);
		}
		return s;
	},
	isNullOrEmpty: function(s) {
		if(s == null || s.length == 0) {
			return true;
		}
		return false;
	}
}, false);