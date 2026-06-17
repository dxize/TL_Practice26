import {
    ResponsiveContainer,
    AreaChart,
    Area,
    XAxis,
    YAxis,
    Tooltip,
    CartesianGrid,
} from "recharts";
import type { PriceChange } from "../../../../models";
import styles from "./RateChart.module.scss";

type RateChartProps = {
    chartData: PriceChange[];
    chartLoading: boolean;
    chartError: string | null;
    periodMinutes: number;
    onPeriodChange: (minutes: number) => void;
};

const formatTime = (dateTime: string): string => {
    const date = new Date(dateTime);
    return date.toLocaleTimeString("en-GB", { timeZone: "UTC" });
};

export const RateChart = ({
    chartData,
    chartLoading,
    chartError,
    periodMinutes,
    onPeriodChange,
}: RateChartProps) => {
    const periods = [5, 4, 3, 2, 1];

    return (
        <div className={styles.container}>
            <div className={styles.periodSwitcher}>
                {periods.map((p) => (
                    <button
                        key={p}
                        type="button"
                        className={
                            p === periodMinutes
                                ? `${styles.periodButton} ${styles.periodButtonActive}`
                                : styles.periodButton
                        }
                        onClick={() => onPeriodChange(p)}
                    >
                        {p} MIN
                    </button>
                ))}
            </div>

            <div className={styles.chartArea}>
                {chartLoading && chartData.length === 0 && (
                    <div className={styles.placeholder}>Loading chart...</div>
                )}

                {chartError && chartData.length === 0 && (
                    <div className={styles.placeholder}>{chartError}</div>
                )}

                {!chartLoading && !chartError && chartData.length === 0 && (
                    <div className={styles.placeholder}>
                        No data for the selected period
                    </div>
                )}

                {chartData.length > 0 && (
                    <ResponsiveContainer width="100%" height={200}>
                        <AreaChart data={chartData}>
                            <CartesianGrid strokeDasharray="3 3" vertical={false} />
                            <XAxis
                                dataKey="dateTime"
                                tickFormatter={formatTime}
                                tick={{ fontSize: 11 }}
                            />
                            <YAxis
                                domain={["auto", "auto"]}
                                tick={{ fontSize: 11 }}
                                width={45}
                            />
                            <Tooltip
                                labelFormatter={(label) =>
                                    new Date(String(label)).toUTCString()
                                }
                            />
                            <Area
                                type="monotone"
                                dataKey="price"
                                stroke="#1a73e8"
                                fill="#1a73e8"
                                fillOpacity={0.15}
                            />
                        </AreaChart>
                    </ResponsiveContainer>
                )}
            </div>

            {chartError && chartData.length > 0 && (
                <div className={styles.autoRefreshError}>
                    Update failed: {chartError}
                </div>
            )}
        </div>
    );
};
