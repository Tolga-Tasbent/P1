int type() {
  int ret=16;
  if (difficulty<1) {
    ret=int(random(3))+14;
  }
  else if (difficulty>1 && difficulty<2) {
    ret= (int(random(5))+12);
  }
  else if (difficulty>2 && difficulty<3) {
    ret= (int(random(8))+9);
  }
  else  if (difficulty>3 && difficulty<4) {
    ret = (int(random(13))+4);
  }
  else if (difficulty>4 && difficulty < 6) {
    ret = (int(random(17)));
  }
  else if (difficulty>6 && difficulty<7) {
    float difchange = random(5);
    if (difchange > 2) {
      ret = int(random(13));
    }
    else {
      ret = (int(random(17)));
    }
  }
  else if (difficulty>7 && difficulty<8) {
    float difchange = random(5);
    if (difchange > 2) {
      ret = int(random(8));
    }
    else {
      ret = (int(random(17)));
    }
  }
  else if (difficulty>8 && difficulty<9) {
    float difchange = random(5);
    if (difchange > 1) {
      ret = int(random(5));
    }
    else {
      ret = (int(random(17)));
    }
  }
  else if (difficulty>9) {
    float difchange = random(5);
    if (difchange > 2) {
      ret = int(random(3));
    }
    else {
      ret = (int(random(17)));
    }
  } 
  return ret;
}

