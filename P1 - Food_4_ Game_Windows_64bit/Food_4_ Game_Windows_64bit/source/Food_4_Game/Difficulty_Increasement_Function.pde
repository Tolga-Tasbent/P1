int difficultyInc() {
  int retValue =0;
  if (difficulty > 7 && difficulty < 9) {
    retValue =  1;
  }
  else if (difficulty > 9 && difficulty < 10) {
    retValue =  2;
  }
  else if (difficulty > 10 && difficulty < 11) {
    retValue =  3;
  }
  else if (difficulty > 11 && difficulty < 12) {
    retValue =  4;
  }
  else if (difficulty > 12 && difficulty < 13) {
    retValue =  5;
  }
  else if (difficulty > 13 && difficulty < 14) {
    retValue =  6;
  }
  else if (difficulty > 14 && difficulty < 15) {
    retValue =  7;
  }
  else if (difficulty > 15) {
    retValue =  int(difficulty/2);
  }
  return retValue;
}

