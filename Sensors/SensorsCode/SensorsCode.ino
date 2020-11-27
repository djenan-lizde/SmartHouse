#include "DHT.h"
#include <MQ2.h>
#include <Servo.h>

#define DHTTYPE DHT11
#define SIGNAL_PIN 4 //PIR sensor
#define LED_PIN 13 //LED light

//change this with the pin that you use
int pin = A0;
int servoPin = 8;
int lpg, co, smoke;

DHT dht(2,DHTTYPE); //pin num 2 DHT11
Servo servo;

void setup()
{
  Serial.begin(9600);
  
  pinMode(SIGNAL_PIN, INPUT);
  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, LOW);
  
  servo.attach(servoPin);
  servo.write(0);
  dht.begin();
  mq2.begin();   
}

void loop(){
   float h = dht.readHumidity();
   float t = dht.readTemperature();

   Serial.println("Current Humidity =");
   Serial.println(h);

   Serial.println("Current temperature =");
   Serial.println(t);

   if(t > (float)35){
    servo.write(90);
   }
   else{
    servo.write(0);
   }

   if(digitalRead(SIGNAL_PIN)==HIGH) {
      Serial.println("Movement detected.");
      digitalWrite(LED_PIN, HIGH);
   } 
   else {
      Serial.println("Did not detect movement.");
      digitalWrite(LED_PIN, LOW);
   }

   float* values= mq2.read(true);
  
   //lpg = values[0];
   lpg = mq2.readLPG();
   //co = values[1];
   co = mq2.readCO();
   //smoke = values[2];
   smoke = mq2.readSmoke();

   delay(5000);
}
