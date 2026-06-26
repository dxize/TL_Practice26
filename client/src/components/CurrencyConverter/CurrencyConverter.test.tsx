import { describe, test, expect, vi, beforeEach } from "vitest";
import { render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { CurrencyConverter } from "./CurrencyConverter";
import * as apiClient from "../../api/apiClient";
import type { Currency, PriceChange } from "../../models";

vi.mock("recharts", async (importOriginal) => {
    const actual = await importOriginal<typeof import("recharts")>();
    return {
        ...actual,
        ResponsiveContainer: ({ children }: { children: React.ReactNode }) => (
            <div data-testid="responsive-container">{children}</div>
        ),
    };
});

vi.mock("../../api/apiClient", () => ({
    ApiClient: {
        fetchCurrencies: vi.fn(),
        fetchPriceHistory: vi.fn(),
    },
}));

const mockCurrencies: Currency[] = [
    { code: "CAD", name: "Canadian Dollar", description: "", symbol: "$" },
    { code: "PLN", name: "Polish Zloty", description: "", symbol: "zł" },
    { code: "AUD", name: "Australian Dollar", description: "", symbol: "$" },
];

const mockPriceHistory: PriceChange[] = [
    {
        toCurrencyCode: "PLN",
        fromCurrencyCode: "CAD",
        price: 2.90,
        dateTime: "2026-05-21T03:40:44Z",
    },
    {
        toCurrencyCode: "PLN",
        fromCurrencyCode: "CAD",
        price: 2.95,
        dateTime: "2026-05-21T03:40:54Z",
    },
];

describe("CurrencyConverter UI States", () => {
    beforeEach(() => {
        vi.clearAllMocks();
    });

    test("shows success state and elements after data load", async () => {
        vi.mocked(apiClient.ApiClient.fetchPriceHistory).mockResolvedValue({
            result: mockPriceHistory,
            errorMessage: "",
        });

        render(<CurrencyConverter currencies={mockCurrencies} />);

        await waitFor(() => {
            const selects = screen.getAllByRole("combobox");
            expect(selects).toHaveLength(2);
        });
    });

    test("shows chart error if fetchPriceHistory fails on first load", async () => {
        vi.mocked(apiClient.ApiClient.fetchPriceHistory).mockResolvedValue({
            result: [],
            errorMessage: "Failed to fetch price history",
        });

        render(<CurrencyConverter currencies={mockCurrencies} />);

        await waitFor(() => {
            expect(screen.getByText("Failed to fetch price history")).toBeInTheDocument();
        });
    });

    test("calculates result when user types amount", async () => {
        vi.mocked(apiClient.ApiClient.fetchPriceHistory).mockResolvedValue({
            result: mockPriceHistory,
            errorMessage: "",
        });
        const user = userEvent.setup();

        render(<CurrencyConverter currencies={mockCurrencies} />);

        await waitFor(() => {
            const inputs = screen.getAllByRole("textbox");
            expect(inputs[1]).toHaveValue("2.95");
        });

        const inputs = screen.getAllByRole("textbox");
        const amountInput = inputs[0];
        const resultInput = inputs[1];
        await user.clear(amountInput);
        await user.type(amountInput, "10");

        await waitFor(() => {
            expect(resultInput).toHaveValue("29.50");
        });
    });

    test("shows period switcher buttons", async () => {
        vi.mocked(apiClient.ApiClient.fetchPriceHistory).mockResolvedValue({
            result: mockPriceHistory,
            errorMessage: "",
        });

        render(<CurrencyConverter currencies={mockCurrencies} />);

        await waitFor(() => {
            expect(screen.getByText("5 MIN")).toBeInTheDocument();
        });

        expect(screen.getByText("4 MIN")).toBeInTheDocument();
        expect(screen.getByText("3 MIN")).toBeInTheDocument();
        expect(screen.getByText("2 MIN")).toBeInTheDocument();
        expect(screen.getByText("1 MIN")).toBeInTheDocument();
    });
});