LinkMeUI.Utils.ScrollTracker = {
    TrackFunction : function(elem ,offset) {
        this.GetWindowSize = function() {
	        if (self.innerWidth) {
		        return {width: self.innerWidth, height: self.innerHeight};
	        } else if (document.documentElement && document.documentElement.clientWidth) {
		        return {width: document.documentElement.clientWidth, height: document.documentElement.clientHeight};
	        } else if (document.body) {
		        return {width: document.body.clientWidth, height: document.body.clientHeight};
	        } else return null;
        }
        
        this.trackF = function() {
            var scrollTop = this.GetCurrentScrollPos();
            var plusOffset = 0;
            var wDim = this.GetWindowSize();
            var eDim = this.elem.getDimensions();            
            
            if(this.offset) {
                plusOffset = this.offset; 
            } else {
                plusOffset = (wDim.height / 2) - (eDim.height / 2);
            }
            
            this.elem.setStyle({
                top: scrollTop + plusOffset + 'px', 
                left: (wDim.width / 2) - (eDim.width / 2) + 'px'
            });
        }
        
        this.GetCurrentScrollPos = function() {
            return parseInt((document.documentElement && document.documentElement.scrollTop) ? document.documentElement.scrollTop : document.body.scrollTop);
        }
        
        this.elem = $(elem);        
        this.offset = offset;        
        this.eventListener = this.trackF.bindAsEventListener(this);        
        Event.observe(window, 'scroll', this.eventListener);
        Event.observe(window, 'resize', this.eventListener);
        window.scrollBy(0, 1);        
        window.scrollBy(0, -1);
    },

    StartTrack : function(elem, offset) {
        var f = new this.TrackFunction(elem, offset);
        elem.TrackFunction = f;
    },
    
    StopTrack : function(elem) {
        Event.stopObserving(window, 'scroll', elem.TrackFunction.eventListener);
        Event.stopObserving(window, 'resize', elem.TrackFunction.eventListener);
    }
}

