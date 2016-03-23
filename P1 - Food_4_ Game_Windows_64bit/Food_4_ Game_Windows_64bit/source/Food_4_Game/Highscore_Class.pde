class highscore {

  highscore() {
  }
  int temp0, temp1, temp2, temp3, temp4, temp5;

  void manage() {
    if (finalScore > pos4.getInt(1) && finalScore < pos3.getInt(1)) {
      highscores.setInt(4, "score", finalScore);
      saveTable(highscores, "data/highscores.csv");
      newHSentry = true;
    }
    else if (finalScore > pos3.getInt(1) && finalScore < pos2.getInt(1)) {
      temp3 = pos3.getInt(1);
      pos4.setInt(1, temp3) ;
      highscores.setInt(3, "score", finalScore);
      saveTable(highscores, "data/highscores.csv");
      newHSentry = true;
    }
    else if (finalScore > pos2.getInt(1) && finalScore < pos1.getInt(1)) {
      temp3 = pos3.getInt(1);
      pos4.setInt(1, temp3) ;
      temp2 = pos2.getInt(1);
      pos3.setInt(1, temp2);
      highscores.setInt(2, "score", finalScore);
      saveTable(highscores, "data/highscores.csv");
      newHSentry = true;
    }
    else if (finalScore > pos1.getInt(1) && finalScore < pos0.getInt(1)) {
      temp3 = pos3.getInt(1);
      pos4.setInt(1, temp3) ;
      temp2 = pos2.getInt(1);
      pos3.setInt(1, temp2);
      temp1 = pos1.getInt(1);
      pos2.setInt(1, temp1);
      highscores.setInt(1, "score", finalScore);
      saveTable(highscores, "data/highscores.csv");
      newHSentry = true;
    }
    else if (finalScore > pos0.getInt(1)) {
      temp3 = pos3.getInt(1);
      pos4.setInt(1, temp3) ;
      temp2 = pos2.getInt(1);
      pos3.setInt(1, temp2);
      temp1 = pos1.getInt(1);
      pos2.setInt(1, temp1);
      temp0 = pos0.getInt(1);
      pos1.setInt(1, temp0);
      highscores.setInt(0, "score", finalScore);
      saveTable(highscores, "data/highscores.csv");
      newHSentry = true;
    }
  }
}

