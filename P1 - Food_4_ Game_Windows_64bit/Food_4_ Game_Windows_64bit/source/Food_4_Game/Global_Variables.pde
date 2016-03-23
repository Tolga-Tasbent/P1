import ddf.minim.*;

Catcher catcher;    
Timer timer;        
Timer timer2;
Drop[] drops;       
Drop[] drops2; 
Menu main;
Menu sub1;
Menu sub2;
AudioPlayer [] sound = new AudioPlayer[9];
AudioPlayer [] gOsound = new AudioPlayer[1];
AudioPlayer startup;
AudioPlayer music;
Minim minim;
Minim minim2;

int score = 0;
int finalScore;
int soundNum = 0;
int totalDrops = 0;
int slide = 0;

boolean GameOver;
boolean restart;
boolean lessDrops = false;
boolean evenLessDrops = false;
boolean difplus = false;
boolean difplus2 = false;
boolean mainMenu=true;
boolean goToMainMenu;
boolean subMenu1;
boolean subMenu2;
boolean goToSubMenu1;
boolean goToSubMenu2;
boolean muteSound;
boolean muteMusic;
boolean introduction;
boolean newHSentry = false;

PImage bg;
PImage [] objects = new PImage[17];
PImage trash;
PImage trash2;
PImage cursor;
PImage logo;
PImage soundmute;
PImage musicmute;
PImage [] introductionSlides = new PImage[6];


float difficulty;
float subscorepos1 = -1000;
float subscorepos2 = -100;
float subscoretransp = 0;
float subscoresize = 30;

Table highscores;
Table settings;
TableRow pos0, pos1, pos2, pos3, pos4, pos5; 
highscore hs;
TableRow soundSettings;

String subscore = "0";

