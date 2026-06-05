import { describe, test, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MoreAboutCurrency } from "./MoreAboutCurrency";

describe("MoreAboutCurrency", () => {
    test("renders button", () => {
        render(<MoreAboutCurrency />);

        expect(screen.getByRole("button", { name: /PLN\/JPY: about/i })).toBeInTheDocument();
    });

    test("description is hidden by default", () => {
        render(<MoreAboutCurrency />);

        expect(screen.queryByText(/Polish zloty/i)).not.toBeInTheDocument();
        expect(screen.queryByText(/Japanese yen/i)).not.toBeInTheDocument();
    });

    test("shows description after click", async () => {
        const user = userEvent.setup();

        render(<MoreAboutCurrency />);

        const button = screen.getByRole("button", { name: /PLN\/JPY: about/i });

        await user.click(button);

        expect(screen.getByText(/Polish zloty/i)).toBeInTheDocument();
        expect(screen.getByText(/official currency and legal tender of Poland/i)).toBeInTheDocument();

        expect(screen.getByText(/Japanese yen/i)).toBeInTheDocument();
        expect(screen.getByText(/official currency of Japan/i)).toBeInTheDocument();
    });

    test("hides description after second click", async () => {
        const user = userEvent.setup();

        render(<MoreAboutCurrency />);

        const button = screen.getByRole("button", { name: /PLN\/JPY: about/i });

        await user.click(button);

        expect(screen.getByText(/Polish zloty - PLN - zł/i)).toBeInTheDocument();

        await user.click(button);

        expect(screen.queryByText(/Polish zloty - PLN - zł/i)).not.toBeInTheDocument();
        expect(screen.queryByText(/Japanese yen - JPY - ¥/i)).not.toBeInTheDocument();
    });
});