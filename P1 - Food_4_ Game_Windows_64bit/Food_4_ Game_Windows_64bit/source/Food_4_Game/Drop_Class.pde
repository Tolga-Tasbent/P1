class Drop {

  float x, y;   
  float speed; 
  float r;     
  float size;
  int type;
  PShape objects;

  Drop() {
    r = 30;                 
    x = random(380);     
    y = -r*4;              
    type = type();
    speed = (18-type+difficultyInc())/2;   
    size = 250-random(100);
  }

  void move() {
    y += speed;
  }

  boolean reachedBottom() {
    if (y > height + r*4) { 
      for (int i=0; i<totalDrops; i++) {
        drops[i].caught();
      }
      return true;
    } 
    else {
      return false;
    }
  }

  void display(PImage [] objects) {

    image(objects[type], x, y, size, size);
  }

  void caught() {
    speed = 0; 
    y = - 1000;
  }
}

