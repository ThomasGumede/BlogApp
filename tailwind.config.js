/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Views/**/*.cshtml", "./Pages/**/*.cshtml"],
  theme: {
    extend: {
      colors: {
        // Configure your color palette here
        transparent: "transparent",
        current: "currentColor",
        glass: "rgba(255, 255, 255, 0.1)",
        "h-color": "#080e34",
        "btn-color": "#3056d3",
        "p-color": "#606970",
      },
    },
    fontFamily: {
      sans: ["Roboto", "sans-serif"],
    },
  },
  plugins: [],
};
