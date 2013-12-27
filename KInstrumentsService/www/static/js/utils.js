function navball_setup( canv )
{
  var ctx = canv.getContext("2d");
  ndata = new Object();
  ndata.canv = canv;
  ndata.pitch = 10;
  ndata.roll = 15;
  ndata.yaw = 0;
  ndata.ctx = ctx;

  ndata.bezel = new Image();
  ndata.bezel.src = "static/bezel.png";

  ndata.bezel.onload = function() {
    canv.width = ndata.bezel.width;
    canv.height = ndata.bezel.height; 
    ctx.drawImage(ndata.bezel, 0,0, ndata.bezel.width, ndata.bezel.height);
  }

  ndata.hor = new Image();
  ndata.hor.src = "static/horizon.png";

  ndata.hor.onload = function() {
    navball_once(ndata);
  }

  return ndata;
}

function d2r( deg )
{
  return Math.PI / 180 * deg;
}

function navball_once( ndata )
{
  ndata.ctx.save();
  ndata.ctx.clearRect(0, 0, ndata.canv.width, ndata.canv.height);
  
  pfact = 1.15;
  
  // look at my embarassinly ham-fisted math!

  ndata.roll = ((ndata.roll + 180) % 360)-180;

  if ( ndata.pitch > 270 ) {
    ndata.pitch -= 360;
  } else {
    if ( ndata.pitch < -90 ) {
      ndata.pitch += 360;
    }
  }

  roll = ndata.roll;
  pitch = ndata.pitch;

  if ( pitch > 90 ) {
    pitch = 180 - pitch;
    roll = 180 - roll;
  }
  if ( pitch < -90 ) {
      roll = 180 - roll;
  }

  $("#rolltext").text( roll );
  $("#pitchtext").text( pitch );

  ndata.ctx.translate( ndata.canv.width/2, ndata.canv.height/2);
  ndata.ctx.rotate( d2r(roll) );

  ndata.ctx.translate( 0, (pitch * pfact) );
  ndata.ctx.drawImage( ndata.hor, -ndata.hor.width/2, -ndata.hor.height/2, ndata.hor.width, ndata.hor.height );

  ndata.ctx.beginPath();
  ndata.ctx.arc(0,90*pfact,4,0, d2r(360));
  ndata.ctx.strokeStyle = "#ffffff";
  ndata.ctx.lineWidth = 3;
  ndata.ctx.stroke();

  ndata.ctx.beginPath();
  ndata.ctx.arc(0,-90*pfact,4,0, d2r(360));
  ndata.ctx.strokeStyle = "#ffffff";
  ndata.ctx.lineWidth = 3;
  ndata.ctx.stroke();

  ndata.ctx.restore();
  ndata.ctx.drawImage(ndata.bezel, 0,0, ndata.bezel.width, ndata.bezel.height);

}
