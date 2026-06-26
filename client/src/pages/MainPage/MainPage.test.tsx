import { describe, test, expect, vi, beforeEach } from "vitest";
import { render, screen, waitFor } from "@testing-library/react";
import { MainPage } from "./MainPage";
import * as apiClient from "../../api/apiClient";

vi.mock("../../api/apiClient", () => ({
    ApiClient: {
        fetchCurrencies: vi.fn(),
        fetchPriceHistory: vi.fn(),
    },
}));

describe("MainPage UI States", () => {
    beforeEach(() => {
        vi.clearAllMocks();
    });

    test("shows loading state initially", () => {
        vi.mocked(apiClient.ApiClient.fetchCurrencies).mockReturnValue(new Promise(() => { }));

        render(<MainPage />);

        expect(screen.getByText(/loading/i)).toBeInTheDocument();
    });

    test("shows error card if fetchCurrencies fails", async () => {
        vi.mocked(apiClient.ApiClient.fetchCurrencies).mockResolvedValue({
            result: [],
            errorMessage: "Network Error",
        });

        render(<MainPage />);

        await waitFor(() => {
            expect(screen.getByText(/COULD NOT GET DATA/i)).toBeInTheDocument();
        });
    });
});
