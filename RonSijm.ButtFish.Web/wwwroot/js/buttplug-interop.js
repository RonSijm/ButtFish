// Buttplug.io JavaScript Interop for Blazor
// This module provides a bridge between Blazor C# and the Buttplug.io JavaScript library

let buttplugClient = null;
let devices = [];

// Import buttplug from lib folder
const buttplugPromise = import('../lib/buttplug/buttplug.mjs');

export async function connect(serverUrl) {
    try {
        const { ButtplugClient, ButtplugBrowserWebsocketClientConnector } = await buttplugPromise;
        
        buttplugClient = new ButtplugClient("ButtFish Web Client");
        
        // Set up event handlers
        buttplugClient.addListener('deviceadded', (device) => {
            console.log(`Device added: ${device.name}`);
            updateDeviceList();
        });
        
        buttplugClient.addListener('deviceremoved', (device) => {
            console.log(`Device removed: ${device.name}`);
            updateDeviceList();
        });
        
        buttplugClient.addListener('disconnect', () => {
            console.log('Disconnected from Intiface Central');
            devices = [];
        });
        
        const connector = new ButtplugBrowserWebsocketClientConnector(serverUrl);
        await buttplugClient.connect(connector);
        
        console.log('Connected to Intiface Central');
        return true;
    } catch (error) {
        console.error('Failed to connect to Intiface Central:', error);
        return false;
    }
}

export async function disconnect() {
    if (buttplugClient && buttplugClient.connected) {
        await buttplugClient.disconnect();
        buttplugClient = null;
        devices = [];
    }
}

export async function startScanning() {
    if (buttplugClient && buttplugClient.connected) {
        await buttplugClient.startScanning();
        console.log('Started scanning for devices');
    }
}

export async function stopScanning() {
    if (buttplugClient && buttplugClient.connected) {
        await buttplugClient.stopScanning();
        console.log('Stopped scanning for devices');
    }
}

function updateDeviceList() {
    if (buttplugClient && buttplugClient.connected) {
        devices = Array.from(buttplugClient.devices).map((device, index) => ({
            index: index,
            name: device.name
        }));
    }
}

export function getDevices() {
    updateDeviceList();
    return devices;
}

export async function vibrate(deviceIndex, intensity, durationMs) {
    if (!buttplugClient || !buttplugClient.connected) {
        console.warn('Not connected to Intiface Central');
        return;
    }
    
    const deviceArray = Array.from(buttplugClient.devices);
    if (deviceIndex < 0 || deviceIndex >= deviceArray.length) {
        console.warn(`Invalid device index: ${deviceIndex}`);
        return;
    }
    
    const device = deviceArray[deviceIndex];
    
    try {
        // Start vibration
        await device.vibrate(intensity);
        
        // Wait for duration
        if (durationMs > 0) {
            await new Promise(resolve => setTimeout(resolve, durationMs));
            // Stop vibration
            await device.stop();
        }
    } catch (error) {
        console.error('Error vibrating device:', error);
    }
}

