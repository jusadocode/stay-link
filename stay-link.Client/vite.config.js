
import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    base: './',  // Relative paths for assets in the dist folder
    build: {
        outDir: 'dist',  // Ensure the output is in the 'dist' folder
    }
    
})
