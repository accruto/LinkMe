
//standard image

    img0off = new Image();
    img0off.src = "img/signin_btn.gif";
    img1off = new Image();
    img1off.src = "img/netmod_jointoday.gif";
    img2off = new Image();
    img2off.src = "img/empmod_jointoday.gif";

//on rollover image

if (document.images) {

    img0on = new Image();
    img0on.src = "img/signin_btn_over.gif";
    img1on = new Image();
    img1on.src = "img/netmod_jointoday_over.gif";
    img2on = new Image();
    img2on.src = "img/empmod_jointoday_over.gif";

}

// Function to 'activate' images.
function imgOn(imgName) {
        if (document.images) {
            document[imgName].src = eval(imgName + "on.src");
        }
}

// Function to 'deactivate' images.
function imgOff(imgName) {
        if (document.images) {
            document[imgName].src = eval(imgName + "off.src");
        }
}

function popUpDemo(url) {
	window.open(url,'Sample','toolbar=no,width=740,height=491,left=100,top=100,status=no,scrollbars=no,resize=no');
	return false;
}
