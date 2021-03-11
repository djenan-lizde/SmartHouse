import React from 'react';
import { View, Text, StyleSheet } from 'react-native';

const AboutUsScreen = () => {
    return (
      <View style={styles.container}>
        <Text>About us Screen</Text>
      </View>
    );
};

export default AboutUsScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1, 
    alignItems: 'center', 
    justifyContent: 'center'
  },
});