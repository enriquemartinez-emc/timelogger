import { Card } from "react-bootstrap";
import { Bar } from "react-chartjs-2";

interface ChartProps {
  chartData: any;
}

export default function Chart({ chartData }: ChartProps) {
  return (
    <Card>
      <Card.Body>
        <Bar
          data={chartData}
          options={{
            indexAxis: "y" as const,
            plugins: {
              title: {
                display: true,
                text: "Cryptocurrency prices",
              },
              legend: {
                display: true,
                position: "bottom",
              },
            },
          }}
        />
      </Card.Body>
    </Card>
  );
}
