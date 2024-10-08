/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    screens: {
      sm: "480px",
      md: "768px",
      lg: "1020px",
      xl: "1440px",
    },
    extend: {
      colors: {
        lightBlue: "hsl(217, 60%, 80%)",
        darkBlue: "hsl(213.86, 58.82%, 46.67%)",
        lightGreen: "hsl(99, 47%, 65%)",
        ashyBlack: "hsl(214, 12%, 11%)",
        lightBlack: "hsl(212, 13%, 19%)"
      },
      fontFamily: {
        sans: ["Poppins", "sans-serif"],
      },
      spacing: {
        180: "32rem",
      },
      backgroundColor: {
        'default': "hsl(214, 12%, 11%)",
      }
    },
  },
  plugins: [],
};

