import styles from "./SwapButton.module.scss";

type SwapButtonProps = {
    onClick: () => void;
};

export const SwapButton = ({ onClick }: SwapButtonProps) => {
    return (
        <button
            className={styles.button}
            onClick={onClick}
            type="button"
        >
            ⇅
        </button>
    );
};
