
import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import basicSsl from '@vitejs/plugin-basic-ssl'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin(), basicSsl()],
    base: './',  // Relative paths for assets in the dist folder
    build: {
        outDir: 'dist',  // Ensure the output is in the 'dist' folder
    },
    server: {
        https: true
    }
    
    
})
