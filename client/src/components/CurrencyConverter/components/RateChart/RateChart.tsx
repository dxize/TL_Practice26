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

const PERIOD_OPTIONS = [5, 4, 3, 2, 1] as const;

type RateChartProps = {
    chartData: PriceChange[];
    chartLoading: boolean;
    chartError: string | null;
    periodMinutes: number;
    onPeriodChange: (minutes: number) => void;
};

const formatTime = (dateTime: string): string => {
    const date = new Date(dateTime);
    const hours = String(date.getUTCHours()).padStart(2, "0");
    const minutes = String(date.getUTCMinutes()).padStart(2, "0");
    const seconds = String(date.getUTCSeconds()).padStart(2, "0");
    return `${hours}:${minutes}:${seconds}`;
};

const formatTooltipDate = (dateTime: string): string => {
    const date = new Date(dateTime);
    return date.toUTCString();
};

type TooltipPayloadItem = {
    value: number;
    payload: PriceChange;
};

const ChartTooltip = ({
    active,
    payload,
}: {
    active?: boolean;
    payload?: TooltipPayloadItem[];
}) => {
    if (!active || !payload || payload.length === 0) {
        return null;
    }

    const point = payload[0];
    return (
        <div className={styles.tooltip}>
            <div className={styles.tooltipDate}>
                {formatTooltipDate(point.payload.dateTime)}
            </div>
            <div className={styles.tooltipValue}>
                <span className={styles.tooltipDot} />
                {point.value}
            </div>
        </div>
    );
};

export const RateChart = ({
    chartData,
    chartLoading,
    chartError,
    periodMinutes,
    onPeriodChange,
}: RateChartProps) => {
    const isFirstLoad = chartLoading && chartData.length === 0;
    const isFirstError = chartError !== null && chartData.length === 0;
    const isEmpty = !chartLoading && chartError === null && chartData.length === 0;
    const isAutoRefreshError = chartError !== null && chartData.length > 0;

    return (
        <div className={styles.container}>
            <div className={styles.periodSwitcher}>
                {PERIOD_OPTIONS.map((p) => (
                    <button
                        key={p}
                        type="button"
                        className={`${styles.periodButton} ${p === periodMinutes ? styles.periodButtonActive : ""}`}
                        onClick={() => onPeriodChange(p)}
                    >
                        {p} MIN
                    </button>
                ))}
            </div>

            <div className={styles.chartArea}>
                {isFirstLoad && (
                    <div className={styles.placeholder}>
                        <div className={styles.spinner} />
                        Loading chart…
                    </div>
                )}

                {isFirstError && (
                    <div className={styles.placeholder}>
                        <div className={styles.errorIcon}>⚠</div>
                        {chartError}
                    </div>
                )}

                {isEmpty && (
                    <div className={styles.placeholder}>
                        No data for the selected period
                    </div>
                )}

                {chartData.length > 0 && (
                    <ResponsiveContainer width="100%" height={200}>
                        <AreaChart data={chartData}>
                            <defs>
                                <linearGradient id="priceGradient" x1="0" y1="0" x2="0" y2="1">
                                    <stop offset="0%" stopColor="#1a73e8" stopOpacity={0.3} />
                                    <stop offset="100%" stopColor="#1a73e8" stopOpacity={0.05} />
                                </linearGradient>
                            </defs>
                            <CartesianGrid
                                strokeDasharray="3 3"
                                vertical={false}
                                stroke="#e8e8e8"
                            />
                            <XAxis
                                dataKey="dateTime"
                                tickFormatter={formatTime}
                                tick={{ fontSize: 11, fill: "#999" }}
                                axisLine={false}
                                tickLine={false}
                            />
                            <YAxis
                                domain={["auto", "auto"]}
                                tick={{ fontSize: 11, fill: "#999" }}
                                axisLine={false}
                                tickLine={false}
                                width={45}
                            />
                            <Tooltip content={<ChartTooltip />} />
                            <Area
                                type="monotone"
                                dataKey="price"
                                stroke="#1a73e8"
                                strokeWidth={2}
                                fill="url(#priceGradient)"
                                dot={{ r: 3, fill: "#1a73e8", strokeWidth: 0 }}
                                activeDot={{ r: 5, fill: "#1a73e8", stroke: "#fff", strokeWidth: 2 }}
                            />
                        </AreaChart>
                    </ResponsiveContainer>
                )}
            </div>

            {isAutoRefreshError && (
                <div className={styles.autoRefreshError}>
                    ⚠ Update failed: {chartError}
                </div>
            )}
        </div>
    );
};
