import { describe, test, expect, vi, beforeEach } from "vitest";
import { render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { CurrencyConverter } from "./CurrencyConverter";
import * as apiClient from "../../api/apiClient";
import type { Currency, PriceChange } from "../../models";

vi.mock("../../api/apiClient", () => ({
    fetchCurrencies: vi.fn(),
    fetchPriceChange: vi.fn(),
}));

const mockCurrencies: Currency[] = [
    { code: "CAD", name: "Canadian Dollar", description: "", symbol: "$" },
    { code: "PLN", name: "Polish Zloty", description: "", symbol: "zł" },
    { code: "AUD", name: "Australian Dollar", description: "", symbol: "$" },
];

const mockPriceChange: PriceChange = {
    toCurrencyCode: "PLN",
    fromCurrencyCode: "CAD",
    price: 2.95,
    dateTime: "2026-05-21T03:40:54Z"
};

describe("CurrencyConverter UI States", () => {
    beforeEach(() => {
        vi.clearAllMocks();
    });

    test("shows loading state initially", () => {
        vi.mocked(apiClient.fetchCurrencies).mockReturnValue(new Promise(() => { }));

        render(<CurrencyConverter />);

        expect(screen.getByText(/loading/i)).toBeInTheDocument();
    });

    test("shows error card if fetchCurrencies fails", async () => {
        vi.mocked(apiClient.fetchCurrencies).mockRejectedValue(new Error("Network Error"));

        render(<CurrencyConverter />);

        await waitFor(() => {
            expect(screen.getByText(/COULD NOT GET DATA/i)).toBeInTheDocument();
        });
    });

    test("shows success state and elements after data load", async () => {
        vi.mocked(apiClient.fetchCurrencies).mockResolvedValue(mockCurrencies);
        vi.mocked(apiClient.fetchPriceChange).mockResolvedValue(mockPriceChange);

        render(<CurrencyConverter />);

        await waitFor(() => {
            expect(screen.queryByText(/loading/i)).not.toBeInTheDocument();
        });

        const selects = screen.getAllByRole("combobox");
        expect(selects).toHaveLength(2);
    });

    test("shows toast error if fetchPriceChange fails", async () => {
        vi.mocked(apiClient.fetchCurrencies).mockResolvedValue(mockCurrencies);
        vi.mocked(apiClient.fetchPriceChange).mockRejectedValue(new Error("Failed to fetch price"));

        render(<CurrencyConverter />);

        await waitFor(() => {
            expect(screen.getByText("Failed to fetch price")).toBeInTheDocument();
        });
    });

    test("calculates result when user types amount", async () => {
        vi.mocked(apiClient.fetchCurrencies).mockResolvedValue(mockCurrencies);
        vi.mocked(apiClient.fetchPriceChange).mockResolvedValue(mockPriceChange);
        const user = userEvent.setup();

        render(<CurrencyConverter />);

        await waitFor(() => {
            expect(screen.queryByText(/loading/i)).not.toBeInTheDocument();
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
});