class Catcher {
  float r;
  float x, y;

  Catcher(float tempR) {
    r = tempR;
    x = 0;
    y = 0;
  }

  void setLocation(float tempX, float tempY) {
    x = tempX;
    y = tempY;
  }

  void display() {
  }

  boolean intersect(Drop d) {
    float distance = dist(x, y, d.x, d.y); 
    if (distance < r + d.r  && mouseButton == LEFT) { 
      return true;
    } 
    else {
      return false;
    }
  }
}

