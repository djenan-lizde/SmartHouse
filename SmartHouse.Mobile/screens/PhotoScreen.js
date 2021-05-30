import React from 'react';
import { View, Text, StyleSheet, Image } from 'react-native';
import Arduino from '../assets/Arduino.jpg';
import ESP32 from '../assets/ESP32.jpg';

const PhotoScreen = () => {
    return (
      <View style={styles.container}>
        <Text style={styles.text}>Arduino uno - PIR sensor</Text>
        <Image
          style={styles.logo}
          source={Arduino}
        />
        <Text style={styles.text}>ESP32 Wroom - DHT11, Servo</Text>
        <Image
          style={styles.logo}
          source={ESP32}
        />
      </View>
    );
};

export default PhotoScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1, 
    alignItems: 'center', 
    justifyContent: 'center'
  },
  logo: {
    width: 200,
    height: 300,
    marginBottom: 30
  },
  text:{
    marginBottom:5,
    fontSize:20
  }
});