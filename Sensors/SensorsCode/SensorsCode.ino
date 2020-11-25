#include "DHT.h"
#include <MQ2.h>

#define DHTTYPE DHT11
#define SIGNAL_PIN 4
#define LED_PIN 13

//change this with the pin that you use
int pin = A0;
int lpg, co, smoke;

DHT dht(2,DHTTYPE);

void setup()
{
  Serial.begin(9600);
  
  pinMode(SIGNAL_PIN, INPUT);
  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, LOW);
  
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

   delay(2000);
}
