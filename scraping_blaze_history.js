const historyDiv = document.getElementById("history");
const betAmounts = [];
const dates = [];
const betDivs = historyDiv.querySelectorAll(".bet");
betDivs.forEach((div) => {
  const betAmount = div.querySelector(".bet-amount").textContent;
  betAmounts.push(betAmount);
  const date = div.querySelector("p").textContent;
  dates.push(date);
});

var script = document.createElement('script');
script.src = 'https://unpkg.com/xlsx/dist/xlsx.full.min.js';
script.onload = function() {
const workbook = XLSX.utils.book_new();
const worksheet = XLSX.utils.aoa_to_sheet([
  ["Bet Amounts", "Dates"], // Cabeçalho da planilha
  ...betAmounts.map((betAmount, index) => [betAmount, dates[index]]), // Dados
]);

XLSX.utils.book_append_sheet(workbook, worksheet, "Dados");
XLSX.writeFile(workbook, "dados.xlsx");
};
document.head.appendChild(script);